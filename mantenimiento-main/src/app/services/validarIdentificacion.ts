import { AbstractControl } from '@angular/forms';
let tipoIdentificacion=0;
export function validatorIdentificaction(control: AbstractControl) {
    if (control.value!=null){
        if(control.value?.length == 10 && tipoIdentificacion == 1){
            return null ;
        } 
        else if(control.value?.length == 13 && tipoIdentificacion == 2){
            return null ;
        }
        else  if(tipoIdentificacion >2){
            return null ;
        }
        return { numeroIdentificacion: true };
      
    } else  return { numeroIdentificacion: true };
   
}


export function validatorTipoIdentificaction(control: AbstractControl) {
    tipoIdentificacion=control.value;
    return null;
   
}