package ec.fin.crea.ad.Services;

import ec.fin.crea.ad.Models.RespuestaServicio;
import ec.fin.crea.ad.Models.Usuario;


public interface IActiveDirectoryService {
	
	RespuestaServicio validarUsuarioAD(Usuario usuario);
	
}
