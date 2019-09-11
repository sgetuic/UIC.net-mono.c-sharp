package de.stacksmashers.pushserver.receiving;

import com.amazonaws.services.iot.client.AWSIotException;
import com.amazonaws.services.iot.client.AWSIotQos;
import de.stacksmashers.pushserver.config.Configuration;
import de.stacksmashers.pushserver.config.ConfigurationLoader;
import de.stacksmashers.pushserver.connecting.ConnectionClient;
import de.stacksmashers.pushserver.message.RestCallSubscription;


/**
 * Service for subscribing on a topic
 * Publishes on the topics will trigger backchannel function
 */
public class SubscriptionService {

    private static SubscriptionService instance = null;
    private final ConnectionClient client;
    private final Configuration configuration;
    private final String TOPIC = "backchannel";

    /**
     * Getter method for the singleton
     * @return singleton instance
     */
    public static synchronized SubscriptionService get() {
        if (instance == null) {
            instance = new SubscriptionService();
        }
        return instance;

    }

    /**
     * private to prohibit constructor calls
     */
    private SubscriptionService() {
        this.client = ConnectionClient.getInstance();
        configuration = new ConfigurationLoader().loadConfig();
    }

    /**
     * starts the subscription to the backchannel topics set in the configuration
     */
    public void initiateDataStoreSunscription() {
        try {
            //   client.subscribe(new DataStorageSubscription(configuration.backTopic(), AWSIotQos.QOS1));
            client.subscribe(new RestCallSubscription(configuration.backTopic(), AWSIotQos.QOS1));
        } catch (AWSIotException e) {
            e.printStackTrace();
        }
    }

}
