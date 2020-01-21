package de.stacksmashers.pushserver.message;

import com.amazonaws.services.iot.client.AWSIotMessage;
import com.amazonaws.services.iot.client.AWSIotQos;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import org.apache.logging.log4j.core.util.datetime.FastDateFormat;

import java.util.Locale;

/**
 * Basically the same as {@link AWSIotMessage} except that it logs the events
 */
public class LoggingIotMessage extends AWSIotMessage {

    private final static FastDateFormat formater = FastDateFormat.getInstance("dd.MM.yyyy HH:mm:ss z", Locale.GERMANY);

    private static final Logger logger = LogManager.getLogger(LoggingIotMessage.class);

    public LoggingIotMessage(String topic, AWSIotQos qos, String payload) {
        super(topic, qos, payload);
    }

    @Override
    public void onSuccess() {
        logger.info(formater.format(System.currentTimeMillis()) + ": publish successfully sent: " + getStringPayload());
    }

    @Override
    public void onFailure() {
        logger.info(formater.format(System.currentTimeMillis()) + ": publish failed for " + getStringPayload());
    }

    @Override
    public void onTimeout() {
        logger.info(formater.format(System.currentTimeMillis()) + ": publish timeout for " + getStringPayload());
    }


}
