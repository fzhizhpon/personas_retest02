package ec.fin.crea.ad.Validations;

import javax.validation.ConstraintValidator;
import javax.validation.ConstraintValidatorContext;

public class CampoObligatorio implements ConstraintValidator<ICampoObligatorio, String> {

	@Override
	public boolean isValid(String value, ConstraintValidatorContext context) {
		
		if(value == null || value.isEmpty() || value.length() > 100) {
			return false;
		}
		
		return true;
	}

}
