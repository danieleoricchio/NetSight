/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package test1;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;

/**
 *
 * @author oricchio_daniele
 */
class StreamGobbler extends Thread {

    InputStream is;
    String type;
    String textStream;

    StreamGobbler(InputStream is, String type) {
        this.is = is;
        this.type = type;
    }

    public void run() {
        try {
            boolean firstErr = false;
            textStream = "";
            InputStreamReader isr = new InputStreamReader(is);
            BufferedReader br = new BufferedReader(isr);
            String line = null;
            while ((line = br.readLine()) != null) {
                if (type.equalsIgnoreCase("ERROR")) {
                    return;
                }
                textStream = textStream + line + "\r\n";

            }
        } catch (IOException ioe) {
            return;
        }
    }

    public String getTextStream() {
        return textStream;
    }

}
