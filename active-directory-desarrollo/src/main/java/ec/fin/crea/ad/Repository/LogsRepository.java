package ec.fin.crea.ad.Repository;

import java.time.LocalDateTime;
import java.time.ZoneOffset;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.http.HttpEntity;
import org.springframework.http.HttpHeaders;
import org.springframework.http.HttpMethod;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Service;
import org.springframework.web.client.RestTemplate;

@Service
public class LogsRepository implements ILogsRepository {
		
	@Value("${app.datalust.seq.token}")
	private String datalustToken;
	
	@Value("${app.datalust.seq.url}")
	private String datalustUrl;	
	
	@Override
	public void info(String msg) {

        String json = "{\"@t\":\""  + LocalDateTime.now(ZoneOffset.UTC) +  "\",\"@m\":\"" +  msg +  "\", \"@l\":\"informational\"}";              
		enviarLog(json);
	}
	
	@Override
	public void warning(String msg) {
		
        String json = "{\"@t\":\""  + LocalDateTime.now(ZoneOffset.UTC) +  "\",\"@m\":\"" +  msg +  "\", \"@l\":\"warning\"}";
		enviarLog(json);		
	}
	
	@Override
	public void error(String msg) {
		
        String json = "{\"@t\":\""  + LocalDateTime.now(ZoneOffset.UTC) +  "\",\"@m\":\"" +  msg +  "\", \"@l\":\"error\"}";
		enviarLog(json);		
	}	
	
	
	private void enviarLog(String json) {
		try {
	        HttpHeaders headers = new HttpHeaders();
	        headers.add("X-Seq-ApiKey", datalustToken);
	        			
	        HttpEntity<String> entity = new HttpEntity<>(json, headers);
	        
	        ResponseEntity<String> response = new RestTemplate().exchange(datalustUrl, HttpMethod.POST, entity, String.class);	       
	        			
		}catch(Exception exc) {
			System.out.println("ERROR! al enviar logs " + exc);
		}
	}
	
}
