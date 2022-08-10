package ec.fin.crea.ad.Models;

import ec.fin.crea.ad.Validations.ICampoObligatorio;

public class Usuario {

	@ICampoObligatorio
	private String nombreCuenta;

	@ICampoObligatorio
	private String contrasenia;

	public Usuario(String nombreCuenta, String contrasenia) {
		super();
		this.nombreCuenta = nombreCuenta;
		this.contrasenia = contrasenia;
	}

	public String getNombreCuenta() {
		return nombreCuenta;
	}

	public void setNombreCuenta(String nombreCuenta) {
		this.nombreCuenta = nombreCuenta;
	}

	public String getContrasenia() {
		return contrasenia;
	}

	public void setContrasenia(String contrasenia) {
		this.contrasenia = contrasenia;
	}

}