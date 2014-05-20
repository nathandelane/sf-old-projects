package com.nathandelane.begprog.mmui;

import java.awt.Dimension;
import java.awt.GridLayout;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.PrintWriter;
import java.util.ArrayList;
import java.util.List;

import javax.swing.BoxLayout;
import javax.swing.JButton;
import javax.swing.JFileChooser;
import javax.swing.JFrame;
import javax.swing.JPanel;
import javax.swing.JToggleButton;

public class UserInterface extends JFrame {

  private static final long serialVersionUID = 1L;
  
  private List<JToggleButton> toggleButtons;

  public UserInterface() {
    super("Map Maker - Nathandelane Beginning Programming");
    
    toggleButtons = new ArrayList<JToggleButton>();
    
    buildUi();
  }
  
  private void buildUi() {
    setSize(new Dimension(1200, 800));
//    setResizable(false);
    setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
    
    final JPanel panel1 = new JPanel();
    panel1.setLayout(new GridLayout(20, 20));

    final BoxLayout boxLayout = new BoxLayout(this.getContentPane(), BoxLayout.Y_AXIS);
    
    setLayout(boxLayout);
    
    for (int i = 0; i < 30; i++) {
      for (int j = 0; j < 40; j++) {
        final JToggleButton button = new JToggleButton();
        
        panel1.add(button);
        
        toggleButtons.add(button);
      }
    }
    
    add(panel1);
    
    final JButton button = new JButton("Save");
    button.addActionListener(new ActionListener() {

      @Override
      public void actionPerformed(ActionEvent arg0) {
        final JFileChooser fc = new JFileChooser();
        final int result = fc.showSaveDialog(null);
        
        if (result == JFileChooser.APPROVE_OPTION) {
          final File file = fc.getSelectedFile();
          
          saveFile(file);
        }
      }
      
      private void saveFile(File file) {
        try {
          final FileOutputStream fos = new FileOutputStream(file, false);
          final PrintWriter w = new PrintWriter(fos);
          
          w.append("size:20,20\n");
          w.append("start:0,0\n");
          w.append("end:19,19\n");
          
          for (JToggleButton next : toggleButtons) {
            w.append(next.isSelected() ? 'X' : '0');            
          }
          
          w.close();
          fos.close();
        }
        catch (FileNotFoundException e) {
          e.printStackTrace();
        }
        catch (IOException e) {
          e.printStackTrace();
        }
        
      }
      
    });
    
    add(button);
    pack();
  }

}
