import { Directive, ElementRef, Input, Renderer2, SecurityContext } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { LanguagesService } from 'src/app/services/common/languages/languages.service';

@Directive({
	selector: '[lang]'
})
export class LanguageDirective {

	@Input() langLabel!: string;

	constructor(
		private el: ElementRef,
		private _langService: LanguagesService,
		private renderer: Renderer2,
		private sanitizer: DomSanitizer,
	) {	}

	ngOnChanges() {
		this.setLanguage()
	}

	setLanguage() {
		this._langService.get(this.langLabel).subscribe(api => {
			if(api.resultado == null || api.resultado == undefined) return;

			this.renderer.setProperty(
				this.el.nativeElement,
				'innerHTML',
				this.sanitizer.sanitize(SecurityContext.HTML, api.resultado)
			);
		})
	}

}
