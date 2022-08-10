const isNullOrEmpty = (text: string) => {
	return text.length == 0 || text == null || text == undefined;
}

const filterBuilder = (baseUrl: string, json: any) => {
	if(baseUrl.charAt(baseUrl.length - 1) != '?') baseUrl = `${baseUrl}?`

	Object.entries(json).forEach((entry) => {
		const [key, value] = entry;
		if(typeof(value) == 'string' && !isNullOrEmpty(value)) {
			baseUrl = `${baseUrl}${key}=${value}&`
		}
		else if(Array.isArray(value)) {
			value.forEach(val => {
				baseUrl = `${baseUrl}${key}=${val}&`
			});
		}
		else if(value != null) {
			baseUrl = `${baseUrl}${key}=${value}&`
		}
	});

	return baseUrl;
}

export default filterBuilder;
