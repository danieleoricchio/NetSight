/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package test1;

/**
 *
 * @author oricchio_daniele
 */
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;

public class RunnableHelper {

    private String errorStream = "";
    private String outputStream = "";
    private String errorRAS = "";

    public int runFile(String pathfile) {

        int exitVal = 0;
        String errors = "";

        try {

            Runtime rt = Runtime.getRuntime();
            Process proc = rt.exec(pathfile);

            StreamGobbler errorGobbler = new StreamGobbler(proc.getErrorStream(), "ERROR");
            StreamGobbler outputGobbler = new StreamGobbler(proc.getInputStream(), "OUTPUT");

            errorGobbler.start();
            outputGobbler.start();

            exitVal = proc.waitFor();
            
            Thread.sleep(1000);
            errorStream = errorGobbler.getTextStream();
            outputStream = outputGobbler.getTextStream();

            errors = errors + "Process exitValue: " + exitVal;

        } catch (Throwable t) {

            exitVal = -1;

        }

        return exitVal;

    }

    public String getErrorStream() {
        return errorStream;
    }

    public String getOutputStream() {
        return outputStream;
    }

}
