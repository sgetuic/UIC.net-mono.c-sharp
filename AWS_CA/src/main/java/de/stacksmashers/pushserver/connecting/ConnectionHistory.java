package de.stacksmashers.pushserver.connecting;

import org.apache.logging.log4j.core.util.datetime.FastDateFormat;

import java.util.LinkedList;
import java.util.Locale;

/**
 * Basically a List of Events that can be printed as HTML
 */
public class ConnectionHistory {

    private final static int SIZE = 2000;
    private final static FastDateFormat formater = FastDateFormat.getInstance("dd.MM.yyyy HH:mm:ss z", Locale.GERMANY);
    private final static LinkedList<HistoryEntry> entries = new LinkedList<>();


    public ConnectionHistory() {
    }


    public void add(long time, String msg) {
        if (entries.size() >= SIZE) {
            entries.removeFirst();
        }
        entries.add(new HistoryEntry(time, msg));
    }


    public String toHtml() {

        StringBuilder sb = new StringBuilder();
        for (HistoryEntry historyEntry : entries) {
            sb.append("<tr align=\"left\"><th>")
                    .append(formater.format(historyEntry.time))
                    .append("</th><th>")
                    .append(historyEntry.msg.replaceAll("\n", "<br>"))
                    .append("</th></tr>");
        }
        return sb.toString();
    }


    private class HistoryEntry {
        private final long time;
        private final String msg;

        HistoryEntry(long time, String msg) {
            this.time = time;
            this.msg = msg;
        }
    }
}
