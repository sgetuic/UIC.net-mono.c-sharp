package de.stacksmashers.pushserver.rest;


import de.stacksmashers.pushserver.config.Configuration;
import de.stacksmashers.pushserver.config.ConfigurationLoader;
import de.stacksmashers.pushserver.config.InternalSetting;
import de.stacksmashers.pushserver.connecting.ConnectionHistory;
import de.stacksmashers.pushserver.pushing.PushService;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import javax.ws.rs.*;
import javax.ws.rs.core.Response;

/**
 *  The JAX-RS Resource that handles the REST API calls
 */
@Path("/iot")
public class RestResource {

    private static final PushService pushService = PushService.get();
    private static final Logger logger = LogManager.getLogger(RestResource.class);
    private static final ConnectionHistory histy = new ConnectionHistory();

    private final Configuration configuration;


    RestResource() {
        configuration = new ConfigurationLoader().loadConfig();
    }


    @POST
    @Path("/push")
    public Response post(String msg) {
        try{
            logger.trace("Received REST Request for pushing. payload: {}", msg);
            String result = "Push with payload: " + msg;
            String topic = configuration.pushTopic().replaceAll("\\{serialid}", InternalSetting.getSerialid());
            pushService.push(topic, msg);
            return Response.status(200).entity(result).build();
        }catch(Exception e){
            e.printStackTrace();
            return Response.status(500).entity("An Exception was thrown while processing PUSh Request").build();
        }
    }


    @POST
    @Path("/init")
    public Response init( @FormParam("serialid") String serialid,@FormParam("edms") String edms) {
        logger.error("Received REST Request for Initiation. serialid: {} and edms: {}", serialid, edms);
        String result = "Initiation with: " + serialid;
        InternalSetting.setSerialid(serialid); //important must be set before pushing
        pushService.push(configuration.initTopic().replaceAll("\\{serialid}", serialid), edms);
        return Response.status(200).entity(result).build();
    }


    @GET
    @Path("/history")
    public Response getHistory() {
        logger.trace("GET HISTORY");
        String answer = "<html><body><table align=\"left\" width=\"80%\">"
                + "<tr align=\"left\"><th>Time</th><th>Event</th></tr>"
                + histy.toHtml() +
                "</table></body></html>";
        return Response.status(200).entity(answer).build();
    }
}
