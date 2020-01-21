package de.stacksmashers.pushserver.message;

import com.amazonaws.services.iot.client.AWSIotMessage;
import com.amazonaws.services.iot.client.AWSIotQos;
import com.amazonaws.services.iot.client.AWSIotTopic;
import com.fasterxml.jackson.core.JsonFactory;
import com.fasterxml.jackson.core.JsonGenerator;
import de.stacksmashers.pushserver.config.Configuration;
import de.stacksmashers.pushserver.config.ConfigurationLoader;
import de.stacksmashers.pushserver.connecting.ConnectionHistory;
import org.apache.http.HttpResponse;
import org.apache.http.client.HttpClient;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.entity.StringEntity;
import org.apache.http.impl.client.HttpClientBuilder;
import org.apache.http.util.EntityUtils;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import java.io.IOException;
import java.io.StringWriter;

import static org.apache.http.entity.ContentType.APPLICATION_JSON;


/**
 * Extension of the {@link AWSIotTopic} that stores the events in the {@link ConnectionHistory} and sends HTTP POST calls with the data
 */
public class RestCallSubscription extends AWSIotTopic {

    private static final Logger logger = LogManager.getLogger(RestCallSubscription.class);
    private static  final JsonFactory factory = new JsonFactory();
    public static final String BACKCHANNEL_PATH = "/backchannel/";

    private final ConnectionHistory connectionHistory = new ConnectionHistory();
    private final Configuration configuration;

    public RestCallSubscription(String topic, AWSIotQos qos) {
        super(topic, qos);
        configuration = new ConfigurationLoader().loadConfig();
    }

    @Override
    public void onMessage(AWSIotMessage message) {

        try {
            final String payload = message.getStringPayload();
            connectionHistory.add(System.currentTimeMillis(), "Received Message on topic: \"" + topic + "\" with payload: \"" + payload + "\"");
            logger.info("Received Message on topic: {} with payload {}", topic, payload);
            String entity = messageToJson(message);
            sendPost(entity);
        } catch (IOException e) {
            logger.error("Error while Http-Post for backchannel.", e);
        }
    }

    /**
     * Encodes the payload and topic of a MQTT Message as JSON
     * @param message the {@link AWSIotMessage} to be parsed as JSON
     * @return A String representation in JSON form
     * @throws IOException
     */
    private String messageToJson(AWSIotMessage message) throws IOException {

       try( StringWriter stringWriter = new StringWriter();
        JsonGenerator generator = factory.createGenerator(stringWriter)){
           generator.writeStartObject();
           generator.writeStringField("topic", message.getTopic());
           generator.writeStringField("payload", message.getStringPayload());
           generator.writeEndObject();
           generator.close();
           stringWriter.close();
           return stringWriter.toString();
       }catch(Exception e){
           logger.error("Error while parsing JSon for POST Request", e);
       }
       return null;
    }


    /**
     * Sends a POST Call with the given JSON String
     * @param entity the JSON String to be send
     */
    public void sendPost(String entity){
        try {
            HttpClient httpClient = HttpClientBuilder.create().build();
            //String postURL = configuration.backchannelUrl();
            String postURL = "http://localhost:" + configuration.portUIC() + BACKCHANNEL_PATH;

            HttpPost post = new HttpPost(postURL);
            StringEntity input = new StringEntity(entity, APPLICATION_JSON);
            post.setEntity(input);
            HttpResponse response = httpClient.execute(post);
            if(response.getStatusLine().getStatusCode()!=200){
                logger.error("Error while pushing informationen to REST API of UIC. RequestEntity was: {}, Answer was: {}", input.toString(),EntityUtils.toString(response.getEntity()) );
            }
        } catch (IOException e) {
            e.printStackTrace();
        }
    }




}
