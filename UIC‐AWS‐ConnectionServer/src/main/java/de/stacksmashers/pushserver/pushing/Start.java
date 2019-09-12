package de.stacksmashers.pushserver.pushing;

import de.stacksmashers.pushserver.config.Configuration;
import de.stacksmashers.pushserver.config.ConfigurationLoader;
import de.stacksmashers.pushserver.receiving.SubscriptionService;
import de.stacksmashers.pushserver.rest.JettyServerThread;
import org.apache.logging.log4j.core.config.Configurator;

import java.io.File;
import java.net.URISyntaxException;

public class Start {

    private static final String CONFIG_FILE_LOGGING = "log4j2.xml";

    public static void main(String[] args) {
        //configure the file for the logging configuration
        configurateLogging();
        //start to subscribe on the backchannel topic
        SubscriptionService.get().initiateDataStoreSunscription();
        Configuration configuration = new ConfigurationLoader().loadConfig();
        PushService pushService = PushService.get();
        pushService.push("status/" + configuration.clientId() , "UAS started.");
        JettyServerThread jettyServerThread = new JettyServerThread();
        jettyServerThread.start();
    }

    private static void configurateLogging() {

        try {
            File rootFolder = new File(Start.class.getProtectionDomain().getCodeSource().getLocation().toURI()).getParentFile();
            File configfile = new File(rootFolder, CONFIG_FILE_LOGGING);
            Configurator.initialize(null, configfile.toURI().getPath());
        } catch (URISyntaxException e) {
            e.printStackTrace();
        }

    }


}
