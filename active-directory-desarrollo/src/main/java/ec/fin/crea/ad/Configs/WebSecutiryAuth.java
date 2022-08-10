package ec.fin.crea.ad.Configs;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.context.annotation.Configuration;
import org.springframework.security.config.annotation.authentication.builders.AuthenticationManagerBuilder;
import org.springframework.security.config.annotation.web.builders.HttpSecurity;
import org.springframework.security.config.annotation.web.configuration.EnableWebSecurity;
import org.springframework.security.config.annotation.web.configuration.WebSecurityConfigurerAdapter;
import org.springframework.security.config.http.SessionCreationPolicy;
import org.springframework.security.crypto.factory.PasswordEncoderFactories;
import org.springframework.security.crypto.password.PasswordEncoder;

import ec.fin.crea.ad.Services.IEncriptacionService;


@Configuration
@EnableWebSecurity
public class WebSecutiryAuth extends WebSecurityConfigurerAdapter {

	@Value("${app.autenticacion.basica.usuario}")
	private String usuario;
	@Value("${app.autenticacion.basica.contrasenia}")
	private String contrasenia;

	@Autowired
	private IEncriptacionService encriptacionService;

	@Override
	protected void configure(HttpSecurity http) throws Exception {
		// http.csrf().disable().authorizeRequests().anyRequest().authenticated().and().httpBasic();
		http.sessionManagement().sessionCreationPolicy(SessionCreationPolicy.STATELESS).and().csrf().disable()
				.authorizeRequests().antMatchers("/validacion-usuario").authenticated().and().httpBasic();
	}

	@Override
	protected void configure(AuthenticationManagerBuilder auth) throws Exception {
		PasswordEncoder encoder = PasswordEncoderFactories.createDelegatingPasswordEncoder();
		auth.inMemoryAuthentication().withUser(usuario).password(encoder.encode(contrasenia)).roles("ADMIN");
	}

}
