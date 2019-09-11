package de.stacksmashers.testclient;

import org.apache.http.HttpResponse;
import org.apache.http.NameValuePair;
import org.apache.http.client.HttpClient;
import org.apache.http.client.entity.UrlEncodedFormEntity;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.message.BasicNameValuePair;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

/**
 * TestClient for performing HTTP Requests for debugging the REST API
 */
public class TestClient {


    private static String EXAMPLE_JSON = "{\n" +
            "    \"name\":\"John\",\n" +
            "    \"age\":30,\n" +
            "    \"cars\": [\n" +
            "        { \"name\":\"Ford\", \"models\":[ \"Fiesta\", \"Focus\", \"Mustang\" ] },\n" +
            "        { \"name\":\"BMW\", \"models\":[ \"320\", \"X3\", \"X5\" ] },\n" +
            "        { \"name\":\"Fiat\", \"models\":[ \"500\", \"Panda\" ] }\n" +
            "    ]\n" +
            " } ";





    public static void main(String[] args) throws IOException {
        String postURL = "http://localhost:8080/iot/push";

        HttpPost post = new HttpPost(postURL);

        List<NameValuePair> params = new ArrayList<NameValuePair>();
        params.add(new BasicNameValuePair("topic", "test/test2"));
        params.add(new BasicNameValuePair("payload", EXAMPLE_JSON));

        UrlEncodedFormEntity ent = new UrlEncodedFormEntity(params, "UTF-8");
        post.setEntity(ent);

        HttpClient client = new DefaultHttpClient();
        HttpResponse responsePOST = client.execute(post);
        System.out.println("ResponseCode: " + responsePOST.getStatusLine());
    }


}
