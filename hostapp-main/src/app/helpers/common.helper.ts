/* eslint-disable @typescript-eslint/ban-types */

export const boolToChar = (obj: Object, properties: string[]) => {
	const newObj: any = {};

	Object.assign(newObj, obj)

	properties.forEach(property => {
		newObj[property] = newObj[property] === true ? '1' : '0'
	});

	return newObj;
}

export const charToBool = (obj: Object, properties: string[]) => {
	const newObj: any = {};

	Object.assign(newObj, obj)

	properties.forEach(property => {
		newObj[property] = newObj[property] == '1'
	});

	return newObj;
}
