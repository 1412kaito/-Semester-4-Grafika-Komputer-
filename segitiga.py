# PENENTUAN TITIK WAJIB CCW
import pygame
import math
from pygame.locals import *
from OpenGL.GL import *
from OpenGL.GLU import *
def main():
    #
    # Ivan Marcellino   6609
    # Johan Poerwanto   6613
    # Katherine Limanu  6621
    # Michael Tenoyo    6635
    #
    pygame.init()
    display = (800,600)
    pygame.display.set_mode(display, DOUBLEBUF|OPENGL)
    gluPerspective(45, (display[0]/display[1]), 0.1, 50.0)
    # glTranslatef(x, y, z)
    glTranslatef(0, 0, -20)    
    running = True
    while running:
        glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT)

        glBegin(GL_POLYGON)
        glColor3f(1,0,0)
        glVertex3f(3,-3,-1)
        glColor3f(0,1,0)
        glVertex3f(0,3,-1)
        glColor3f(0,0,1)
        glVertex3f(-3,-3,-1)
        glEnd()
        pygame.display.flip()
        pygame.time.wait(10)
        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                running=False
                pygame.quit()

main()