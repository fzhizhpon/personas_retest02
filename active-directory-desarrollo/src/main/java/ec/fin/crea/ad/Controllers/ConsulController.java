package ec.fin.crea.ad.Controllers;

import org.springframework.boot.autoconfigure.EnableAutoConfiguration;
//import org.springframework.cloud.client.discovery.EnableDiscoveryClient;
import org.springframework.context.annotation.Configuration;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;


@Configuration
@EnableAutoConfiguration
//@EnableDiscoveryClient
@RestController
public class ConsulController {
	
	
	@RequestMapping("/ping")
	public ResponseEntity<String> myCustomCheck() {
	    String message = "Testing my healh check function";
	    return new ResponseEntity<>(message, HttpStatus.OK);
	}
	
}
