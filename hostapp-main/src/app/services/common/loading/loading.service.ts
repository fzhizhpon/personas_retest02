import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
	providedIn: 'root'
})
export class LoadingService {

	loading$: BehaviorSubject<number[]> = new BehaviorSubject<number[]>([]);

	show(): void {
		const data = this.loading$.value;
		data.push(0)

		this.loading$.next(data)
	}

	hide(): void {
		const data = this.loading$.value;

		if(data.length > 0) {
			data.pop()
		}

		this.loading$.next(data);
	}

}
