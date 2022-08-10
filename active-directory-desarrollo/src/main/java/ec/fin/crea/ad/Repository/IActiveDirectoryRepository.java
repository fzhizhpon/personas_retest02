package ec.fin.crea.ad.Repository;

import javax.naming.ldap.LdapContext;

import org.springframework.stereotype.Repository;


@Repository
public interface IActiveDirectoryRepository {
	
	LdapContext obtenerContextoLdap();
	boolean esUsuarioValido(String nombreUsuario, String contraseniaEncriptada);
	
	
}
