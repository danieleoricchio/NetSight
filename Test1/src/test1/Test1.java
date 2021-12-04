/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package test1;

import java.io.IOException;
import java.net.InetAddress;
import java.net.UnknownHostException;
import java.util.logging.Level;
import java.util.logging.Logger;

/**
 *
 * @author oricchio_daniele
 */
public class Test1 {

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        try {
            Functions.PingPc(InetAddress.getByName("192.168.0.102"));
        } catch (UnknownHostException ex) {
            Logger.getLogger(Test1.class.getName()).log(Level.SEVERE, null, ex);
        } catch (IOException ex) {
            Logger.getLogger(Test1.class.getName()).log(Level.SEVERE, null, ex);
        }

        try {
            Functions.ShowRunningPrograms(InetAddress.getByName("172.16.102.83"));
        } catch (UnknownHostException ex) {
            Logger.getLogger(Test1.class.getName()).log(Level.SEVERE, null, ex);
        }
        String[] programs = {"chrome.exe", "notepadd++.exe"};
        try {
            Functions.ShowSelectedPrograms(InetAddress.getByName("172.16.102.83"), programs);
        } catch (UnknownHostException ex) {
            Logger.getLogger(Test1.class.getName()).log(Level.SEVERE, null, ex);
        }
    }

}
