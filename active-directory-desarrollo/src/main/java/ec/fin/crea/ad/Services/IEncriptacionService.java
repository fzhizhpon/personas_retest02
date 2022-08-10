package ec.fin.crea.ad.Services;

import org.springframework.stereotype.Repository;

@Repository
public interface IEncriptacionService {
	
	String encriptar(String valor);
	String desenciptar(String valor);
	
}
