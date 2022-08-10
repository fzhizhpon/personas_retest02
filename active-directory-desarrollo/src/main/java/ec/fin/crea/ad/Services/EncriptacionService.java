package ec.fin.crea.ad.Services;

import javax.crypto.SecretKey;
import javax.crypto.spec.IvParameterSpec;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;

import ec.fin.crea.ad.Repository.IActiveDirectoryRepository;
import ec.fin.crea.ad.Repository.ILogsRepository;
import ec.fin.crea.ad.Utils.Encriptacion;

@Service
public class EncriptacionService implements IEncriptacionService {

//	@Autowired
//	private JdbcTemplate jdbcConector;

	@Value("${app.encriptacion.llave}")
	private String llave;
	@Value("${app.encriptacion.vector}")
	private String vector;
	
	@Autowired
	private ILogsRepository logsRepository;	

	@Override
	public String encriptar(String valor) {
		SecretKey llaveSecreta = Encriptacion.convertirStringASecretKey(llave);
		IvParameterSpec ivParam = Encriptacion.convertirStringAIvParameter(vector);

		try {
			if(valor == null || valor.isEmpty()) return null;			
			String resultado = Encriptacion.encriptar(valor, llaveSecreta, ivParam);
			return resultado;
		} catch (Exception exc) {
			logsRepository.error(exc.toString());
		}
		return null;
	}

	@Override
	public String desenciptar(String valor) {
		SecretKey llaveSecreta = Encriptacion.convertirStringASecretKey(llave);
		IvParameterSpec ivParam = Encriptacion.convertirStringAIvParameter(vector);

		try {
			if(valor == null || valor.isEmpty()) return null;
			String resultado = Encriptacion.desencriptar(valor, llaveSecreta, ivParam);
			return resultado;
		} catch (Exception exc) {
			logsRepository.error(exc.toString());
		}
		return null;
	}

}
