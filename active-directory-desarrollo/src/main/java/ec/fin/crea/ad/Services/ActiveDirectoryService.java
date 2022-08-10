package ec.fin.crea.ad.Services;

import java.util.Calendar;
import java.util.Date;

import javax.naming.NamingEnumeration;
import javax.naming.NamingException;
import javax.naming.directory.Attributes;
import javax.naming.directory.SearchControls;
import javax.naming.directory.SearchResult;
import javax.naming.ldap.LdapContext;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import ec.fin.crea.ad.Models.RespuestaServicio;
import ec.fin.crea.ad.Models.Usuario;
import ec.fin.crea.ad.Repository.IActiveDirectoryRepository;
import ec.fin.crea.ad.Repository.ILogsRepository;
import ec.fin.crea.ad.Utils.Values;

@Service
public class ActiveDirectoryService implements IActiveDirectoryService {

	@Autowired
	private IActiveDirectoryRepository adRepository;

	@Autowired
	private ILogsRepository logsRepository;

	@Autowired
	private IEncriptacionService encriptacionService;

	@Override
	public RespuestaServicio validarUsuarioAD(Usuario usuario) {

		int codigoRespuesta = 0;
		String mensajeRespuesta = "";

		try {

			LdapContext contextoLdap = adRepository.obtenerContextoLdap();

			SearchControls controlBusqueda = new SearchControls();
			controlBusqueda.setSearchScope(2);
			NamingEnumeration<SearchResult> resultadoBusqueda;

			String usuarioSistema = encriptacionService.desenciptar(usuario.getNombreCuenta());
			int lastIndex = usuarioSistema.lastIndexOf("@");
			if(lastIndex != -1) {
				usuarioSistema = usuarioSistema.substring(0, lastIndex);
			}
			
			resultadoBusqueda = contextoLdap.search("DC=coopcrea,DC=fin,DC=ec",
					"(sAMAccountName=" + usuarioSistema + ")",
					controlBusqueda);

			if (resultadoBusqueda.hasMoreElements()) {
				SearchResult elementoEncontrado = resultadoBusqueda.nextElement();

				if (elementoEncontrado != null) {
					String nombreUsuario = elementoEncontrado.getNameInNamespace();
					Attributes atributosElemento = contextoLdap.getAttributes(nombreUsuario);

					String tiempoCuentaExpira = atributosElemento.get("accountExpires").get().toString();
					final long fileTime = Long.parseLong(tiempoCuentaExpira) / 10000L - 11644473600000L;
					Date fechaExpira = new Date(fileTime - 38600L);

					Calendar calendar = Calendar.getInstance();
					calendar.setTime(fechaExpira);
					calendar.add(10, -24);

					int dias = 1000000;
					Date fechaHoy = new Date();
					if (atributosElemento.get("accountExpires").get().toString().equals("0")) {
						dias = 100000;
					} else {
						dias = (int) ((fechaExpira.getTime() - fechaHoy.getTime()) / 86400000L);
					}

					try {
						// Se logea utilizando el contexto Ldap
						boolean esUsuarioValido = adRepository.esUsuarioValido(nombreUsuario, usuario.getContrasenia());

						if (!esUsuarioValido) {
							if (atributosElemento.get("lockoutTime") != null
									&& atributosElemento.get("lockoutTime").get().equals("0")) {
								codigoRespuesta = Values.COD_CUENTA_CREDENCIALES_INCORRECTOS;
								mensajeRespuesta = Values.MSG_CUENTA_CREDENCIALES_INCORRECTOS;
							} else {
								codigoRespuesta = Values.COD_CUENTA_USUARIO_BLOQUEADO;
								mensajeRespuesta = Values.MSG_CUENTA_USUARIO_BLOQUEADO;
							}
							
							
						} else {

							if (atributosElemento.get("UserAccountControl").get().equals("512")
									|| atributosElemento.get("UserAccountControl").get().equals("66048")) {
								codigoRespuesta = Values.COD_CUENTA_USUARIO_OK;
								mensajeRespuesta = Values.MSG_CUENTA_USUARIO_OK;

								if (dias <= 3 && dias > 0) {
									codigoRespuesta = 80 + dias;
									codigoRespuesta = Values.COD_CUENTA_USUARIO_POR_EXPIRAR;
									mensajeRespuesta = Values.MSG_CUENTA_USUARIO_POR_EXPIRAR;
								}

								if (dias <= 0) {
									codigoRespuesta = Values.COD_CUENTA_USUARIO_EXPIRADA;
									mensajeRespuesta = Values.MSG_CUENTA_USUARIO_EXPIRADA;
								}

								if (atributosElemento.get("lockoutTime") != null
										&& atributosElemento.get("lockoutTime").get().equals("0")) {
									codigoRespuesta = Values.COD_CUENTA_USUARIO_OK;
									mensajeRespuesta = Values.MSG_CUENTA_USUARIO_OK;
								} else {
									codigoRespuesta = Values.COD_CUENTA_USUARIO_BLOQUEADO;
									mensajeRespuesta = Values.MSG_CUENTA_USUARIO_BLOQUEADO;
								}
							}

							if (atributosElemento.get("UserAccountControl").get().equals("16")) {
								codigoRespuesta = Values.COD_CUENTA_USUARIO_BLOQUEADO;
								mensajeRespuesta = Values.MSG_CUENTA_USUARIO_BLOQUEADO;
							}

							if (atributosElemento.get("UserAccountControl").get().equals("2")) {
								codigoRespuesta = Values.COD_CUENTA_USUARIO_DESHABIITADA;
								mensajeRespuesta = Values.MSG_CUENTA_USUARIO_DESHABIITADA;
							}

							if (atributosElemento.get("UserAccountControl").get().equals("8388608")) {
								codigoRespuesta = Values.COD_CUENTA_USUARIO_EXPIRADA;
								mensajeRespuesta = Values.MSG_CUENTA_USUARIO_EXPIRADA;
							}

							if (encriptacionService.desenciptar(usuario.getNombreCuenta()) == null
									|| encriptacionService.desenciptar(usuario.getContrasenia()).trim()
											.isEmpty()) {
								codigoRespuesta = Values.COD_CUENTA_CREDENCIALES_INCORRECTOS;
								mensajeRespuesta = Values.MSG_CUENTA_CREDENCIALES_INCORRECTOS;
							}
							
						}

					} catch (NamingException exc) {
						logsRepository.error(exc.toString());
					}
				}
			} else {
				codigoRespuesta = Values.COD_CUENTA_CREDENCIALES_INCORRECTOS;
				mensajeRespuesta = Values.MSG_CUENTA_CREDENCIALES_INCORRECTOS;
			}

			RespuestaServicio respuesta = new RespuestaServicio();
			respuesta.setCodigo(codigoRespuesta);
			//respuesta.setMensajeTecnico(mensajeRespuesta);
			respuesta.setMensaje(mensajeRespuesta);
			contextoLdap.close();
			return respuesta;

		} catch (NamingException exc) {
			logsRepository.error(exc.toString());
			return null;
		}
	}
}
