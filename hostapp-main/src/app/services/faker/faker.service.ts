import { Injectable } from '@angular/core';

@Injectable({
	providedIn: 'root'
})
export class FakerService {

	private readonly names = [
		'Helen',
		'Patricia',
		'William',
		'Robert',
		'Margaret',
		'Barbara',
		'Deborah',
		'Carol',
		'Elizabeth',
		'John',
		'Linda',
		'David',
		'DeAngelo',
		'Lucila',
		'Marc',
		'Hester',
		'Devon',
		'Paul',
		'Jared',
		'Keyla'
	]

	private readonly surnames = [
		'Klaft',
		'Szerbin',
		'Breault',
		'Zimmermann',
		'Burker',
		'Ciotti',
		'Grieser',
		'Bains',
		'Styborski',
		'Wray',
		'Zirbel',
		'Squier',
		'Redman',
		'McQuaid',
		'Hascup',
		'Ficke',
		'Osazuwa',
		'Stelzl',
		'Beatley',
		'Bielicki',
	]

	getNames(quantity: number): string[] {
		const names = [];

		for(let i = 0; i < quantity; i++) {
			names.push(`${this.names[this.getRandomNumber(0, 9)]} ${this.names[this.getRandomNumber(10, 20)]} ${this.surnames[this.getRandomNumber(0, 9)]} ${this.surnames[this.getRandomNumber(10, 20)]}`)
		}

		return names;
	}

	getRandomNumber(min: number, max: number): number {
		return Math.floor(Math.random() * (max - min)) + min;
	}

}
