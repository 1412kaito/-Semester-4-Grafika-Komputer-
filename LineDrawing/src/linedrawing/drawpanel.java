/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package linedrawing;

import java.awt.Color;
import java.awt.Graphics;
import java.awt.Point;
import java.util.ArrayList;
import javax.swing.*;

/**
 *
 * @author Ivan Marcellino
 */
public class drawpanel extends javax.swing.JPanel {
    public int count;
    /**
     * Creates new form drawpanel
     */
    private window parent;
    private String algo;
    private Point first, second;
    private ArrayList<Point[]> toDraw;
    public drawpanel() {
        initComponents();
        algo="";
        toDraw=new ArrayList<>();
    }
    
    public void isiparent(){
        parent = (window)this.getParent().getParent().getParent().getParent();
    }
    

    @Override
    protected void paintComponent(Graphics grphcs) {
        super.paintComponent(grphcs); 
        //To change body of generated methods, choose Tools | Templates.
        grphcs.setColor(Color.WHITE);
        
        if(first != null && second != null){
            int powerX, powerY;
            int deltaX, deltaY;
            deltaX=first.x-second.x;        
            deltaY=first.y-second.y;
            powerX=deltaX*deltaX; powerY=deltaY*deltaY;
            if(parent.dda){
                drawDDA(powerX, powerY, deltaX, deltaY, grphcs);
            } else if (parent.bersenham){
                if (powerX > powerY){
                    if (first.x > second.x){
                        Bresenham1(grphcs, second.x, second.y, first.x, first.y);
                    } else {
                        Bresenham1(grphcs, first.x, first.y, second.x, second.y);
                    }
                }else {
                    int x0, y0, x1, y1;
                    if(first.y>second.y){
                        Bresenham2(grphcs, second.x, second.y, first.x, first.y);
                    } else {
                        Bresenham2(grphcs, first.x, first.y, second.x, second.y);
                    }
                }
            } else if (parent.circle){
                //JOptionPane.showMessageDialog(this, "Circle!");
                drawCircle(grphcs, first, second);
            } else if (parent.sincos){
                //JOptionPane.showMessageDialog(this, "sincos!");
                drawSincos(grphcs, first, second);

            }
        }
    }
    
    private void cetaktitik(Point a, Point b){
        double threesixty = 2*Math.PI;
        double r;
        r = jarak(a, b);
        System.out.println("actual");
        for (int i = 0; i <= 360; i++) {
            int x, y;
            double kali;
            kali = (i/360.0) * threesixty;
            x = (int)(a.x + r * Math.cos(kali));            
            y = (int)(b.y + r * Math.sin(kali));
            System.out.println("x: "+x+"|y: "+y);
        }
        System.out.println("end");
    }
    
    private void drawPoints(Graphics g, int x, int y, Point p){
        g.drawRect(p.x + x, p.y + y, 1, 1);
        g.drawRect(p.x + y, p.y + x, 1, 1);
        g.drawRect(p.x - y, p.y + x, 1, 1);
        g.drawRect(p.x - x, p.y + y, 1, 1);

        g.drawRect(p.x - x, p.y - y, 1, 1);
        g.drawRect(p.x - y, p.y - x, 1, 1);
        g.drawRect(p.x + y, p.y - x, 1, 1);
        g.drawRect(p.x + x, p.y - y, 1, 1);

    }
    
    private void drawCircle(Graphics g, Point pusat, Point ujung){
        int radius, x, y, error;
        radius = (int)(jarak(pusat, ujung)+0.5);
        x=radius;
        y=0;
        error=0;
        while (x>=y) {
            drawPoints(g, x, y, pusat);
            if (error<=0){
                ++y; 
                error+=2*y+1;
            }
            if (error>0){
                x--;
                error -= 2*x +1;
            }
        }
    }
    
    private double jarak(Point a, Point b){
        double pdx = a.x-b.x; pdx*=pdx;
        double pdy = a.y-b.y; pdy*=pdy;
        return Math.sqrt(pdx+pdy);
    }
    
    private void drawSincos(Graphics g, Point pusat, Point ujung){
        double threesixty = 2*Math.PI;
        double r;
        r = jarak(pusat, ujung);
        for (int i = 0; i <= 360; i++) {
            int x, y;
            double kali;
            kali = (i/360.0) * threesixty;
            System.out.println(kali);
            x = (int)(pusat.x + r * Math.cos(kali));            
            y = (int)(pusat.y + r * Math.sin(kali));
            g.drawRect(x, y, 1, 1);
        }
    }

    private void Bresenham1(Graphics grphcs, int x0, int y0, int x1, int y1) {
        int deltaX, deltaY;
        int step, F, j;
        deltaX = x1-x0;
        deltaY = y1-y0;
        step=1;
        if (deltaY<0){
            step=-1; deltaY=-deltaY;
        }
        F = deltaY+deltaY-deltaX;
        j=y0;
        for (int i = x0; i <= x1; i++) {
            grphcs.drawRect(i, j, 1, 1);
//            if(F<0) F+=deltaY+deltaY;
//            else{
//                j+=step;
//                F+=2*(deltaY-deltaX);
//            }
            if(F>0){
                j+=step;
                F -= - 2*deltaX;
            }
            F += + deltaY+deltaY;
        }
    }

    private void Bresenham2(Graphics grphcs, int x0, int y0, int x1, int y1){
        int dx, dy, step;
        int F, i;
        dx = x1-x0;
        dy = y1-y0;
        step = 1;
        if(dx < 0){
            step=-1; dx=-dx;
        }
        F = dx + dx - dy;
        i = x0;
        for (int j = y0; j <= y1; j++) {
            grphcs.drawRect(i, j, 1, 1);
//            if(F>0){
//                i+=step;
//                F -= (2*dy);
//            }
//            F += dx+dx;
            if(F<0)
                F+=dx+dx;
            else {
                i+=step;
                F+=2*(dx-dy);
            }
        }
    }

    private void drawDDA(int powerX, int powerY, int deltaX, int deltaY, Graphics grphcs) {
        int awalX;
        int awalY;
        int akhirX;
        int akhirY;
        float grad;
        if(powerX>=powerY){
            if(deltaX>0){
                awalX = second.x;
                awalY = second.y;
                akhirX = first.x;
                akhirY = first.y;
            } else {
                awalX = first.x;
                akhirX = second.x;
                awalY = first.y;
                akhirY = second.y;
            }
            grad = ((float)(deltaY))/((float)(deltaX));
            float j=awalY;
            for (int i = awalX; i <= akhirX; i++) {
                grphcs.drawRect(i, (int)(j+0.5), 1, 1);
                j = (j + grad);
            }
        } else {
            if(deltaY>0){
                awalX = second.x;
                awalY = second.y;
                akhirX = first.x;
                akhirY = first.y;
            } else {
                awalX = first.x;
                akhirX = second.x;
                awalY = first.y;
                akhirY = second.y;
            }
            float j=awalX;
            grad = (float)(deltaX)/(float)(deltaY);
            for (int i = awalY; i <= akhirY; i++) {
                grphcs.drawRect((int)(j+0.5), i, 1, 1);
                j = (j + grad);
            }
        }
    }
    
    public void gantiAlgo(String algobaru){
        algo=algobaru;
        ctr_and_algo.setText(algo+" "+count);
        first=null; second=null;
    }
    
    /**
     * This method is called from within the constructor to initialize the form.
     * WARNING: Do NOT modify this code. The content of this method is always
     * regenerated by the Form Editor.
     */
    @SuppressWarnings("unchecked")
    // <editor-fold defaultstate="collapsed" desc="Generated Code">//GEN-BEGIN:initComponents
    private void initComponents() {

        ctr_and_algo = new javax.swing.JTextField();

        setBackground(new java.awt.Color(0, 0, 0));
        setPreferredSize(new java.awt.Dimension(500, 500));
        addMouseListener(new java.awt.event.MouseAdapter() {
            public void mouseClicked(java.awt.event.MouseEvent evt) {
                formMouseClicked(evt);
            }
        });
        setLayout(null);

        ctr_and_algo.setEditable(false);
        ctr_and_algo.setBackground(new java.awt.Color(255, 255, 255));
        add(ctr_and_algo);
        ctr_and_algo.setBounds(0, 0, 90, 20);
    }// </editor-fold>//GEN-END:initComponents

    private void formMouseClicked(java.awt.event.MouseEvent evt) {//GEN-FIRST:event_formMouseClicked
        // TODO add your handling code here:
        //JOptionPane.showMessageDialog(parent, evt.getPoint().toString());
        if (algo==""){
            JOptionPane.showMessageDialog(null, "Pilih algo dulu!");
        } else {
            if (count==0){
                ++count;
                ctr_and_algo.setText(algo+" "+count);
                first = new Point(evt.getPoint());
                //JOptionPane.showMessageDialog(parent, "1");
            } else {
                count++;
                ctr_and_algo.setText(algo+" "+count);

                second = new Point(evt.getPoint());
                if(parent.dda){
                    //JOptionPane.showMessageDialog(this, "DDA!");

                } else if (parent.bersenham){
                    //JOptionPane.showMessageDialog(this, "Bersenham!");

                } else if (parent.circle){
                    //JOptionPane.showMessageDialog(this, "Circle!");

                } else if (parent.sincos){
                    //JOptionPane.showMessageDialog(this, "sincos!");

                }
                //draw!
                count =0;
                this.repaint();
                ctr_and_algo.setText(algo+" "+count);

            }
        }
    }//GEN-LAST:event_formMouseClicked


    // Variables declaration - do not modify//GEN-BEGIN:variables
    private javax.swing.JTextField ctr_and_algo;
    // End of variables declaration//GEN-END:variables
}
