SELECT 
	FECHA = ch3fecha,
	CUENTA = ch3cuent,
	NOMBRE = (SELECT 
				ltrim(rtrim(cm2dcnobr))+ltrim(rtrim(cm2dcnomc))
				FROM cm2 
				WHERE cm2cuenta=ch3cuent),

	CEDULA = (SELECT 
				cm2dccedu 
				FROM cm2 
				WHERE cm2cuenta=ch3cuent),

	PAGO_SOBREGIRO = CONVERT(NUMERIC(16),SUBSTRING(CH3VARIAB,1,16))/100 

	FROM CH3 
	WHERE 
		CH3CUENT IN (SELECT 
						CM2CUENTA  FROM cm2 
						WHERE CM2SCEFEC <= 0 
						AND CM2DCESTA IN ('A','P','S','C','B','I','J','Q','V'))
		AND CH3FECHA BETWEEN @StartDate AND @EndDate
		AND CH3TRANS IN ('0700', '1700', '0770')
		AND CH3CODIGO = 'iok'

	ORDER BY CH3FECHA