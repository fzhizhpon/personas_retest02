package ec.fin.crea.ad.Utils;

import java.security.InvalidAlgorithmParameterException;
import java.security.InvalidKeyException;
import java.security.NoSuchAlgorithmException;
import java.security.SecureRandom;
import java.util.Base64;

import javax.crypto.BadPaddingException;
import javax.crypto.Cipher;
import javax.crypto.IllegalBlockSizeException;
import javax.crypto.KeyGenerator;
import javax.crypto.NoSuchPaddingException;
import javax.crypto.SecretKey;
import javax.crypto.spec.IvParameterSpec;
import javax.crypto.spec.SecretKeySpec;

public class Encriptacion {

	private static String algoritmoCompleto = "AES/CBC/PKCS5Padding";
	private static String algoritmoAbreviado = "AES";
	private static int nivelEncriptado = 256;

	public static SecretKey generarKey() throws NoSuchAlgorithmException {
		KeyGenerator keyGenerator = KeyGenerator.getInstance(algoritmoAbreviado);
		keyGenerator.init(nivelEncriptado);
		SecretKey key = keyGenerator.generateKey();
		return key;
	}

	public static IvParameterSpec generarIv() {
		byte[] iv = new byte[16];
		new SecureRandom().nextBytes(iv);
		return new IvParameterSpec(iv);
	}

	public static String encriptar(String valor, SecretKey llave, IvParameterSpec vectorInicializacion)
			throws NoSuchPaddingException, NoSuchAlgorithmException, InvalidAlgorithmParameterException,
			InvalidKeyException, BadPaddingException, IllegalBlockSizeException {

		Cipher cipher = Cipher.getInstance(algoritmoCompleto);
		cipher.init(Cipher.ENCRYPT_MODE, llave, vectorInicializacion);
		byte[] valorCifrado = cipher.doFinal(valor.getBytes());
		return Base64.getEncoder().encodeToString(valorCifrado);
	}

	public static String desencriptar(String valorCifrado, SecretKey llave, IvParameterSpec vectorInicializacion)
			throws NoSuchPaddingException, NoSuchAlgorithmException, InvalidAlgorithmParameterException,
			InvalidKeyException, BadPaddingException, IllegalBlockSizeException {

		Cipher cipher = Cipher.getInstance(algoritmoCompleto);
		cipher.init(Cipher.DECRYPT_MODE, llave, vectorInicializacion);
		byte[] textoPlano = cipher.doFinal(Base64.getDecoder().decode(valorCifrado));
		return new String(textoPlano);
	}
	
	public static SecretKey convertirStringASecretKey(String key) {
		// decode the base64 encoded string
		byte[] decodedKey = Base64.getDecoder().decode(key);
		// rebuild key using SecretKeySpec
		SecretKey originalKey = new SecretKeySpec(decodedKey, 0, decodedKey.length, algoritmoAbreviado);	
		return originalKey;
	}
	
	public static IvParameterSpec convertirStringAIvParameter(String iv) {
		// decode the base64 encoded string
		byte[] decodedKey = Base64.getDecoder().decode(iv);
		// rebuild key using SecretKeySpec
		IvParameterSpec ivParam = new IvParameterSpec(decodedKey);	
		return ivParam;
	}
	
//	public static SecretKey convertirSecretKeyAString(String key) {
//		// create new key
//		SecretKey secretKey = KeyGenerator.getInstance("AES").generateKey();
//		// get base64 encoded version of the key
//		String encodedKey = Base64.getEncoder().encodeToString(secretKey.getEncoded());
//	}


}
