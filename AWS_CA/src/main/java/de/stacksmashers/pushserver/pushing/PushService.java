package de.stacksmashers.pushserver.pushing;

import com.amazonaws.services.iot.client.AWSIotException;
import de.stacksmashers.pushserver.connecting.ConnectionClient;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

/**
 * Utility class for pushing MQTT Messages to AWS
 */
public class PushService {
    private static final Logger logger = LogManager.getLogger(PushService.class);

    private static PushService instance = null;
    private final ConnectionClient client;

    /**
     * Singleton getter method
     * @return the singleton instance
     */
    public static synchronized PushService get() {
        if (instance == null) {
            instance = new PushService();
        }
        return instance;

    }


    private PushService() {
        this.client = ConnectionClient.getInstance();
    }

    /**
     * Pushes a given message on a given payload
     * @param topic the topic on which the message will be published
     * @param message the message to be pubslihed
     * */
    public void push(String topic, String message) {
        if (topic == null) {
            throw new IllegalArgumentException("Topic must not be null");
        }
        if (message == null) {
            message = "";
        }

        try {
            client.push(topic, message);
        } catch (AWSIotException e) {
            logger.error("Exception while publishing the message", e);
        }
    }
}

