import pygame;
from pygame.locals import *
from OpenGL.GL import *
from OpenGL.GLU import *

def drawFront(x, y, constant_z, size):
    glBegin(GL_LINE_STRIP)
    glVertex3f(x,y,constant_z)
    glVertex3f(x+size,y,constant_z)
    glVertex3f(x+size,y+size,constant_z)
    glVertex3f(x,y+size,constant_z)
    glVertex3f(x,y,constant_z)
    glEnd()

def drawSide(y, z, constant_x, size):
    glBegin(GL_LINE_STRIP)
    glVertex3f(constant_x,y,z)
    glVertex3f(constant_x,y+size,z)
    glVertex3f(constant_x,y+size,z+size)
    glVertex3f(constant_x,y,z+size)
    glVertex3f(constant_x,y,z)
    glEnd()
	
def drawTop(x, z, constant_y, size):
    glBegin(GL_LINE_STRIP)
    glVertex3f(x,constant_y,z)
    glVertex3f(x,constant_y,z+size)
    glVertex3f(x+size,constant_y,z+size)
    glVertex3f(x+size,constant_y,z)
    glVertex3f(x,constant_y,z)
    glEnd()

def drawCube(startX, startY, startZ, size):
    drawFront(startX, startY, startZ, size)
    drawFront(startX, startY, startZ+size, size)
    drawSide(startY, startZ, startX, size)
    drawSide(startY, startZ, startX+size, size)
    drawTop(startX, startZ, startY, size)
    drawTop(startX, startZ, startY+size, size)

def main():
    pygame.init()
    display = (800,600)

    pygame.display.set_mode(display, DOUBLEBUF|OPENGL)
    gluPerspective(45, (display[0]/display[1]), 0.1, 50.0)
    glTranslatef(0 ,0 , -20)
    running = True
    
    glBegin(GL_QUADS)
    glNormal3f(0,0,-1)
    glVertex3f( 0,50, 0)
    glVertex3f(50,50, 0)
    glVertex3f(50, 0, 0)
    glVertex3f( 0, 0, 0)
    glEnd()
    
    while running:
        glBegin(GL_QUADS)
        glNormal3f(0,0,-1)
        glVertex3f( 1,1, 0)
        glVertex3f(1,-1, 0)
        glVertex3f(-1, -1, 0)
        glVertex3f( -1, 1, 0)
        glEnd()
        # glClear(GL_COLOR_BUFFER_BIT|GL_DEPTH_BUFFER_BIT)
        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                running=False
                pygame.quit()
            else:
                pygame.display.flip()
                glClear(GL_COLOR_BUFFER_BIT|GL_DEPTH_BUFFER_BIT)
                glRotatef(16,1,0,0)
                drawCube(-5,-1,0,2)
                drawCube(5,-1,0,2)
                pygame.time.wait(10)
            # else: 
            #     pygame.display.flip()
            #     glClear(GL_COLOR_BUFFER_BIT|GL_DEPTH_BUFFER_BIT)
            #     drawCube(-5,-1,1,2)
            #     drawCube(5,-1,1,2)
        
        
                    


main()
