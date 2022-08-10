package ec.fin.crea.ad.Repository;

import org.springframework.stereotype.Repository;

@Repository
public interface ILogsRepository {

	void info(String msg);
	void warning(String msg);
	void error(String msg);
	
}
