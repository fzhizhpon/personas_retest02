import { Injectable } from '@angular/core';
import { Sesion } from 'src/app/models/Sesion';
// import * as CryptoJS from 'crypto-js';

@Injectable({
	providedIn: 'root'
})
export class LocalStorageService {

	private secretKey;

	constructor() {
		this.secretKey = this.generateSecretKey()
	}

	private generateSecretKey(): string {
		const navigator_info = window.navigator;
		const screen_info = window.screen;
		let uid = navigator_info.mimeTypes.length.toString();
		uid += screen_info.orientation.angle;
		uid += navigator_info.userAgent.replace(/\D+/g, '');
		uid += screen_info.colorDepth;
		uid += navigator_info.plugins.length;
		uid += screen_info.pixelDepth;
		uid += navigator_info.hardwareConcurrency.toLocaleString()

		return uid;
	}

	private getKey(key: string): string {
		// return CryptoJS.SHA224(key).toString();
		return key;
	}

	private decrypt(dataEncrypted: string): string {
		// return CryptoJS.AES.decrypt(dataEncrypted, this.secretKey).toString(CryptoJS.enc.Utf8)
		return dataEncrypted;
	}

	private encrypt(data: unknown): string {
		// return CryptoJS.AES.encrypt(JSON.stringify(data), this.secretKey).toString()
		return JSON.stringify(data);
	}

	getStorage(keyName: string): unknown {
		try {
			const key = this.getKey(keyName)
			const data = window.localStorage.getItem(key);

			if(data) {
				const dataDecrypted = this.decrypt(data);
				return JSON.parse(dataDecrypted) || dataDecrypted;
			}

			return null
		} catch(ex) {
			return null
		}
	}

	setStorage(keyName: string, data: unknown): void {
		const key = this.getKey(keyName)
		const dataEncrypted = this.encrypt(data);

		window.localStorage.setItem(key, dataEncrypted);
	}

	getSesion(): Sesion {
		return <Sesion>(this.getStorage('sesion'));
	}

	clear(keyName: string): void {
		const key = this.getKey(keyName)
		window.localStorage.removeItem(key);
	}

}
