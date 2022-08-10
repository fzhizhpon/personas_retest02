import { animate, style, transition, trigger } from '@angular/animations';
import { Component } from '@angular/core';
import { Router, Event as RouterEvent,
	NavigationStart,
	NavigationEnd,
	NavigationCancel,
	NavigationError } from '@angular/router';
import { LoadingService } from './services/common/loading/loading.service';

@Component({
	selector: 'app-root',
	templateUrl: './app.component.html',
	styleUrls: ['./app.component.scss'],
	animations: [
		trigger(
			'inOutAnimation',
			[
				transition(
					':enter',
					[
						style({ opacity: 0 }),
						animate('0.4s ease-out',
							style({ opacity: 1 }))
					]
				)
			]
		)
	]
})
export class AppComponent {
	title = 'VimaCoop';

	showLoading = false;

	constructor(
		private router: Router,
		private loading: LoadingService,
	) {
		this.loading.loading$.subscribe(loadings => this.showLoading = loadings.length > 0)

		router.events.subscribe((event: RouterEvent) => {
			this.navigationInterceptor(event)
		})

		this.setAutocompleteOff();
	}

	setAutocompleteOff() {
		let body = document.getElementsByTagName('body')[0]

		const observer = new MutationObserver(list => {
			const elements = document.querySelectorAll('[autocomplete="off"]')
			elements.forEach(el => el.setAttribute('autocomplete', `new-${Date.now().toString()}`) )
		});

		const attributes = false;
		const childList = true;
		const subtree = true;

		observer.observe(body, { attributes, childList, subtree });
	}

	navigationInterceptor(event: RouterEvent): void {
		if (event instanceof NavigationStart) {
			this.loading.show()
		}
		if (event instanceof NavigationEnd) {
			this.loading.hide()
		}

		// Set loading state to false in both of the below events to hide the spinner in case a request fails
		if (event instanceof NavigationCancel) {
			this.loading.hide()
		}
		if (event instanceof NavigationError) {
			this.loading.hide()
		}
	}
}
