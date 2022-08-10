package ec.fin.crea.ad.Models;

import java.util.LinkedHashMap;
import java.util.Map;

import com.fasterxml.jackson.annotation.JsonInclude;
import com.fasterxml.jackson.annotation.JsonProperty;

public class RespuestaServicio {
	@JsonInclude(JsonInclude.Include.ALWAYS)
    @JsonProperty("codigo")
	private int codigo;
	
	@JsonInclude(JsonInclude.Include.ALWAYS)
    @JsonProperty("mensaje")
	private String mensaje;
	
//	@JsonInclude(JsonInclude.Include.ALWAYS)
//    @JsonProperty("mensajeTecnico")
//	private String mensajeTecnico;
	
	@JsonInclude(JsonInclude.Include.ALWAYS)
    @JsonProperty("objeto")
	private Object objeto;

	public int getCodigo() {
		return codigo;
	}

	public void setCodigo(int codigo) {
		this.codigo = codigo;
	}

	public String getMensaje() {
		return mensaje;
	}

	public void setMensaje(String mensajeUsuario) {
		this.mensaje = mensajeUsuario;
	}

//	public String getMensajeTecnico() {
//		return mensajeTecnico;
//	}
//
//	public void setMensajeTecnico(String mensajeTecnico) {
//		this.mensajeTecnico = mensajeTecnico;
//	}

	public Object getObjeto() {
		return objeto;
	}

	public void setObjeto(Object objeto) {
		this.objeto = objeto;
	}

	public Map<String, Object> objetToMap() {
		Map<String, Object> mapa = new LinkedHashMap<String, Object>();
		mapa.put("codigo", this.codigo);
		mapa.put("mensaje", this.mensaje);
		//mapa.put("mensajeTecnico", this.mensajeTecnico);
		mapa.put("objeto", this.objeto);
		return mapa;
	}

}
