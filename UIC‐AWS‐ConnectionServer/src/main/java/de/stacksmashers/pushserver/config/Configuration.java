package de.stacksmashers.pushserver.config;

import org.aeonbits.owner.Config;

/**
 * Aeonbitsowner configuration for the Server
 */
@Config.HotReload
@Config.Sources("file:${config}")
public interface Configuration extends Config {

    @Key("region")
    String awsRegion();

    @Key("cert")
    String certificate();

    @DefaultValue("testclientID")
    @Key("clientid")
    String clientId();

    @DefaultValue("data")
    @Key("praefix")
    String praefix();

    @DefaultValue("")
    @Key("private_key_file")
    String privateKey();

    @DefaultValue("8081")
    @Key("port_uic")
    int portUIC();

    @DefaultValue("8080")
    @Key("port_uas")
    int portUAS();

    @DefaultValue("1")
    @Key("qos")
    int qos();

    @DefaultValue("myProject/{serialid}/push")
    @Key("push_topic")
    String pushTopic();

    @DefaultValue("myProject/{serialid}/init")
    @Key("init_topic")
    String initTopic();

    @DefaultValue("myProject/{serialid}/backd")
    @Key("backchannel_topic")
    String backTopic();
}
