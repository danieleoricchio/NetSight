/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package test1;

import java.io.BufferedReader;
import java.io.File;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.InetAddress;
import java.util.logging.Level;
import java.util.logging.Logger;

/**
 *
 * @author oricchio_daniele
 */
public class Functions {

    public static void PingPc(InetAddress address) throws IOException {
        System.out.println("Invio richiesta di ping a: " + address.toString());
        if (address.isReachable(5000)) {
            System.out.println("L'Host è raggiungibile");
        } else {
            System.out.println("L'Host non è raggiungibile");
        }
    }

    public static void ShowRunningPrograms(InetAddress address) {
        try {
            String line;
            Process p = Runtime.getRuntime().exec(System.getenv("windir") + "\\system32\\" + "tasklist.exe");
            BufferedReader input = new BufferedReader(new InputStreamReader(p.getInputStream()));

            while ((line = input.readLine()) != null) {
                System.out.println(line);
            }
            input.close();
        } catch (Exception err) {
            err.printStackTrace();
        }
    }

    public static void ShowSelectedPrograms(InetAddress address, String[] programs) {
        try {
            String line;
            Process p = Runtime.getRuntime().exec(System.getenv("windir") + "\\system32\\" + "tasklist.exe");
            BufferedReader input = new BufferedReader(new InputStreamReader(p.getInputStream()));

            while ((line = input.readLine()) != null) {
                for (int i = 0; i < programs.length; i++) {
                    if (line.contains(programs[i])) {
                        System.out.println(line);
                    }
                }
            }
            input.close();
        } catch (Exception err) {
            err.printStackTrace();
        }
    }

    public static void KillSelectedPrograms(String[] programs) {
        new Thread() {
            @Override
            public void run() {
                try {
                    for (int i = 0; i < programs.length; i++) {
                        Runtime.getRuntime().exec("taskkill.exe /IM " + programs[i]);
                    }
                } catch (IOException ex) {
                    Logger.getLogger(Functions.class.getName()).log(Level.SEVERE, null, ex);
                }
            }
        }.start();
    }

    public static void OpenSelectedProgram() throws IOException {
        RunnableHelper rn = new RunnableHelper();
        
        rn.runFile("src\\ProgramManager\\RunFile.bat");
    }
}
