package de.stacksmashers.pushserver.rest;


import de.stacksmashers.pushserver.config.Configuration;
import de.stacksmashers.pushserver.config.ConfigurationLoader;
import org.eclipse.jetty.server.Server;
import org.eclipse.jetty.servlet.ServletContextHandler;
import org.eclipse.jetty.servlet.ServletHolder;
import org.jboss.resteasy.plugins.server.servlet.HttpServletDispatcher;

/**
 * Thread for the Jetty Server that is handling the REST API
 */
public class JettyServerThread extends Thread {


    static final String APPLICATION_PATH = "/rest";
    static final String CONTEXT_ROOT = "/";
    private final Configuration configuration;

    public JettyServerThread() {
        configuration = new ConfigurationLoader().loadConfig();
    }


    /**
     * Starts the REST API via Jetty Server
     */
    public void run() {
        final int port = configuration.portUAS();
        final Server server = new Server(port);

        // Setup the basic Application "context" at "/".
        // This is also known as the handler tree (in Jetty speak).
        final ServletContextHandler context = new ServletContextHandler(
                server, CONTEXT_ROOT);
        final ServletHolder restEasyServlet = new ServletHolder(
                new HttpServletDispatcher());
        restEasyServlet.setInitParameter("resteasy.servlet.mapping.prefix",
                APPLICATION_PATH);
        restEasyServlet.setInitParameter("javax.ws.rs.Application",
                "de.stacksmashers.pushserver.rest.MessageApplication");
        context.addServlet(restEasyServlet, CONTEXT_ROOT);


        try {
            server.start();
            server.join();
        } catch (Exception e) {
            e.printStackTrace();
        }

    }
}
