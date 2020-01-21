package de.stacksmashers.pushserver.config;

import de.stacksmashers.pushserver.exceptions.NoInitiationException;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

public class InternalSetting {

    private static final Logger logger = LogManager.getLogger(InternalSetting.class);
    private static String serialid;


    public static String getSerialid() throws NoInitiationException {
        if(serialid ==null){
            throw new NoInitiationException("Not initialized yet!");
        }
        return serialid;
    }

    public static void setSerialid(String serialid) {
        InternalSetting.serialid = serialid;
        logger.info("UIC_Id was set to {}", serialid);
    }
}
