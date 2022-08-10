package ec.fin.crea.ad.Repository;

import java.util.Hashtable;

import javax.naming.Context;
import javax.naming.directory.DirContext;
//import javax.naming.directory.DirContext;
import javax.naming.directory.InitialDirContext;
import javax.naming.ldap.InitialLdapContext;
import javax.naming.ldap.LdapContext;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;

import ec.fin.crea.ad.Services.IEncriptacionService;

@Service
public class ActiveDirectoryRepository implements IActiveDirectoryRepository {

	@Autowired
	private IEncriptacionService encriptacionService;

	@Value("${app.active-directory.usuario}")
	private String usuario;
	@Value("${app.active-directory.contrasenia}")
	private String contrasenia;

	@Override
	public LdapContext obtenerContextoLdap() {

		try {
			Hashtable<String, String> configuracionLdap = new Hashtable<String, String>();
			configuracionLdap.put(Context.INITIAL_CONTEXT_FACTORY, "com.sun.jndi.ldap.LdapCtxFactory");
			configuracionLdap.put(Context.PROVIDER_URL, "ldap://srv-dc-01.coopcrea.fin.ec:389");
			configuracionLdap.put(Context.SECURITY_AUTHENTICATION, "simple");
			configuracionLdap.put(Context.SECURITY_PRINCIPAL, encriptacionService.desenciptar(usuario));
			configuracionLdap.put(Context.SECURITY_CREDENTIALS, encriptacionService.desenciptar(contrasenia));
			LdapContext contextoLdap = new InitialLdapContext(configuracionLdap, null);
			return contextoLdap;
		} catch (Exception e) {
			// Guardar Log
			return null;
		}
	}

	@Override
	public boolean esUsuarioValido(String nombreUsuario, String contraseniaEncriptada) {
		try {
			Hashtable<String, String> configuracionAuth = new Hashtable<String, String>();
			configuracionAuth.put(Context.INITIAL_CONTEXT_FACTORY, "com.sun.jndi.ldap.LdapCtxFactory");
			configuracionAuth.put(Context.PROVIDER_URL, "ldap://srv-dc-01.coopcrea.fin.ec:389");
			configuracionAuth.put(Context.SECURITY_AUTHENTICATION, "simple");
			configuracionAuth.put(Context.SECURITY_PRINCIPAL, nombreUsuario);
			configuracionAuth.put(Context.SECURITY_CREDENTIALS, encriptacionService.desenciptar(contraseniaEncriptada));
			// DirContext authContecto = new InitialDirContext(configuracionAuth);
			DirContext context = new InitialDirContext(configuracionAuth);
			context.close();
			return true;
		} catch (Exception exc) {
			// Guardar Log
			return false;
		}

	}

}
