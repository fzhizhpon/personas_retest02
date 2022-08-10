package ec.fin.crea.ad.Controllers;

import javax.validation.Valid;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import ec.fin.crea.ad.Models.RespuestaServicio;
import ec.fin.crea.ad.Models.Usuario;
import ec.fin.crea.ad.Services.IActiveDirectoryService;

@RestController
@RequestMapping("api")
public class ServiceController {
		
	@Autowired
	private IActiveDirectoryService adService;   
    
	@PostMapping("validacion-usuario")	
	public RespuestaServicio index(@RequestBody @Valid Usuario usuario) {
		RespuestaServicio r = adService.validarUsuarioAD(usuario);	
		return r;
	}	

}
