package de.stacksmashers.pushserver.connecting;

import com.amazonaws.services.iot.client.*;
import com.amazonaws.services.iot.client.sample.sampleUtil.SampleUtil;
import de.stacksmashers.pushserver.config.Configuration;
import de.stacksmashers.pushserver.config.ConfigurationLoader;
import de.stacksmashers.pushserver.message.LoggingIotMessage;

/**
 * This class handles the connection to AWS using the official AWS libraries
 */
public class ConnectionClient {


    private static ConnectionClient instance;
    private final Configuration configuration;
    private final AWSIotMqttClient client;
    private final ConnectionHistory connectionHistory = new ConnectionHistory();

    /**
     * priovate to prohibit constructor call from outside (singleton)
     */
    private ConnectionClient() {
        configuration = new ConfigurationLoader().loadConfig();
        String clientEndpoint = configuration.praefix() + ".iot." + configuration.awsRegion() + ".amazonaws.com";       // replace <prefix> and <region> with your own
        String clientId = configuration.clientId();                           // replace with your own client ID. Use unique client IDs for concurrent connections.
        String certificateFile = configuration.certificate();
        String privateKeyFile = configuration.privateKey();
        SampleUtil.KeyStorePasswordPair pair = SampleUtil.getKeyStorePasswordPair(certificateFile, privateKeyFile);
        client = new AWSIotMqttClient(clientEndpoint, clientId, pair.keyStore, pair.keyPassword);
    }

    /**
     * Getter for the singleton instance
     * @return a singleton of this class
     */
    public static ConnectionClient getInstance() {
        if (instance == null) {
            instance = new ConnectionClient();
        }
        return instance;
    }

    /**
     * Pushes a given message on a given payload
     * @param topic the topic on which the message will be published
     * @param message the message to be pubslihed
     * @throws AWSIotException thrown if any exception occurred in the process
     */
    public void push(String topic, String message) throws AWSIotException {
        connectionHistory.add(System.currentTimeMillis(), "PUBLISH on topic: \"" + topic + "\" with payload: \"" + message + "\"");

        if (client.getConnectionStatus() == AWSIotConnectionStatus.DISCONNECTED) {
            client.connect();
        }
        client.publish(new LoggingIotMessage(topic, AWSIotQos.valueOf(configuration.qos()), message));
    }


    /**
     * Subnscribes on the given topic. The messages that are published on this topic will trigger the backchannel function.
     * @param awsIotTopic the topic to be subscribes on
     * @throws AWSIotException thrown if any exception occurred in the process
     */
    public void subscribe(AWSIotTopic awsIotTopic) throws AWSIotException {
        connectionHistory.add(System.currentTimeMillis(), "SUBSCRIBE on topic: \"" + awsIotTopic.getTopic() + "\"");

        if (client.getConnectionStatus() == AWSIotConnectionStatus.DISCONNECTED) {
            client.connect();
        }
        client.subscribe(awsIotTopic);
    }


}
