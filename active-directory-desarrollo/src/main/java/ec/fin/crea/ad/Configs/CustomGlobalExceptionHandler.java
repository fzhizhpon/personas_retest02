package ec.fin.crea.ad.Configs;

import java.util.List;
import java.util.Map;
import java.util.stream.Collectors;

import org.springframework.http.HttpHeaders;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.MethodArgumentNotValidException;
import org.springframework.web.bind.annotation.ControllerAdvice;
import org.springframework.web.context.request.WebRequest;
import org.springframework.web.servlet.mvc.method.annotation.ResponseEntityExceptionHandler;

import ec.fin.crea.ad.Models.RespuestaServicio;


@ControllerAdvice
public class CustomGlobalExceptionHandler extends ResponseEntityExceptionHandler {

	// error handle for @Valid
	@Override
	protected ResponseEntity<Object> handleMethodArgumentNotValid(MethodArgumentNotValidException ex,
			HttpHeaders headers, HttpStatus status, WebRequest request) {

		// Get all errors
		List<String> errors = ex.getBindingResult().getFieldErrors().stream().map(x -> x.getDefaultMessage())
				.collect(Collectors.toList());

		RespuestaServicio resp = new RespuestaServicio();
		resp.setCodigo(status.value());
		resp.setMensaje("Credencial incorrecta.");
		//resp.setMensajeTecnico("Credencial incorrecta.");
		resp.setObjeto(errors);

		Map<String, Object> mapa = resp.objetToMap();

		return new ResponseEntity<>(mapa, headers, HttpStatus.OK);

	}

}
