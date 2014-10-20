Imports System
Imports System.Runtime.InteropServices
Imports SFML
Imports SFML.System
Imports SFML.Window
Imports SFML.Graphics
Imports OpenTK
Imports OpenTK.Graphics


Module OpenGL

    Dim WithEvents window As RenderWindow

    ''' <summary>
    ''' Entry point of application
    ''' </summary>
    Sub Main()

        ' Request a 32-bits depth bufer when creating the window
        Dim contextSettings As New ContextSettings()
        contextSettings.DepthBits = 32

        ' Create main window
        window = New RenderWindow(New VideoMode(800, 600), "SFML graphics with OpenGL (Visual Basic)", Styles.Default, contextSettings)
        window.SetVerticalSyncEnabled(True)

        ' Make it the active window for OpenGL calls
        window.SetActive(True)

        ' Initialize OpenTK
        Toolkit.Init()
        Dim context As GraphicsContext
        context = New GraphicsContext(New ContextHandle(IntPtr.Zero), Nothing)

        ' Create a sprite for the background
        Dim background = New Sprite(New Texture("resources/background.jpg"))

        ' Create a text to display on top of the OpenGL object
        Dim text = New Text("SFML / OpenGL demo", New Font("resources/sansation.ttf"))
        text.Position = New Vector2f(250, 450)
        text.Color = New Color(255, 255, 255, 170)

        ' Load an OpenGL texture
        ' We could directly use a SFML.Graphics.Texture as an OpenGL texture (with its Bind() member function),
        ' but here we want more control on it (generate mipmaps, ...) so we create a new one
        Dim texture = 0
        Using image = New Image("resources/texture.jpg")
            GL.GenTextures(1, texture)
            GL.BindTexture(TextureTarget.Texture2D, texture)
            Glu.Build2DMipmap(TextureTarget.Texture2D, PixelInternalFormat.Rgba, image.Size.X, image.Size.Y, PixelFormat.Rgba, PixelType.UnsignedByte, image.Pixels)
            GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, TextureMagFilter.Linear)
            GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, TextureMinFilter.Linear)
        End Using

        ' Enable Z-buffer read and write
        GL.Enable(EnableCap.DepthTest)
        GL.DepthMask(True)
        GL.ClearDepth(1)

        ' Disable lighting
        GL.Disable(EnableCap.Lighting)

        ' Configure the viewport (the same size as the window)
        GL.Viewport(0, 0, window.Size.X, window.Size.Y)

        ' Setup a perspective projection
        GL.MatrixMode(MatrixMode.Projection)
        GL.LoadIdentity()
        Dim ratio As Single = window.Size.X / window.Size.Y
        GL.Frustum(-ratio, ratio, -1, 1, 1, 500)

        ' Enable 2D Textures
        GL.Enable(EnableCap.Texture2D)

        ' Define a 3D cube (6 faces made of 2 triangles composed by 3 vertices)
        Dim cube = New Single() _
        { _
            -20, -20, -20, 0, 0, _
            -20, 20, -20, 1, 0, _
            -20, -20, 20, 0, 1, _
            -20, -20, 20, 0, 1, _
            -20, 20, -20, 1, 0, _
            -20, 20, 20, 1, 1, _
 _
             20, -20, -20, 0, 0, _
             20, 20, -20, 1, 0, _
             20, -20, 20, 0, 1, _
             20, -20, 20, 0, 1, _
             20, 20, -20, 1, 0, _
             20, 20, 20, 1, 1, _
 _
            -20, -20, -20, 0, 0, _
             20, -20, -20, 1, 0, _
            -20, -20, 20, 0, 1, _
            -20, -20, 20, 0, 1, _
             20, -20, -20, 1, 0, _
             20, -20, 20, 1, 1, _
 _
            -20, 20, -20, 0, 0, _
             20, 20, -20, 1, 0, _
            -20, 20, 20, 0, 1, _
            -20, 20, 20, 0, 1, _
             20, 20, -20, 1, 0, _
             20, 20, 20, 1, 1, _
 _
            -20, -20, -20, 0, 0, _
             20, -20, -20, 1, 0, _
            -20, 20, -20, 0, 1, _
            -20, 20, -20, 0, 1, _
             20, -20, -20, 1, 0, _
             20, 20, -20, 1, 1, _
 _
            -20, -20, 20, 0, 0, _
             20, -20, 20, 1, 0, _
            -20, 20, 20, 0, 1, _
            -20, 20, 20, 0, 1, _
             20, -20, 20, 1, 0, _
             20, 20, 20, 1, 1 _
        }

        ' Enable position and texture coordinates vertex components
        GL.EnableClientState(EnableCap.VertexArray)
        GL.EnableClientState(EnableCap.TextureCoordArray)
        GL.VertexPointer(3, VertexPointerType.Float, 5 * 4, Marshal.UnsafeAddrOfPinnedArrayElement(cube, 0))
        GL.TexCoordPointer(2, TexCoordPointerType.Float, 5 * 4, Marshal.UnsafeAddrOfPinnedArrayElement(cube, 3))

        ' Disable normal and color vertex components
        GL.DisableClientState(EnableCap.NormalArray)
        GL.DisableClientState(EnableCap.ColorArray)

        Dim clock = New Clock()

        ' Start the game loop
        While (window.IsOpen)

            ' Process events
            window.DispatchEvents()

            ' Clear the window
            window.Clear()

            ' Draw background
            window.PushGLStates()
            window.Draw(background)
            window.PopGLStates()

            ' Clear the depth buffer
            GL.Clear(ClearBufferMask.DepthBufferBit)

            ' We get the position of the mouse cursor, so that we can move the box accordingly
            Dim x = Mouse.GetPosition(window).X * 200.0F / window.Size.X - 100.0F
            Dim y = -Mouse.GetPosition(window).Y * 200.0F / window.Size.Y + 100.0F

            ' Apply some transformations
            GL.MatrixMode(MatrixMode.Modelview)
            GL.LoadIdentity()
            GL.Translate(x, y, -100.0F)
            GL.Rotate(clock.ElapsedTime.AsSeconds() * 50, 1.0F, 0.0F, 0.0F)
            GL.Rotate(clock.ElapsedTime.AsSeconds() * 30, 0.0F, 1.0F, 0.0F)
            GL.Rotate(clock.ElapsedTime.AsSeconds() * 90, 0.0F, 0.0F, 1.0F)

            ' Bind the texture
            GL.BindTexture(TextureTarget.Texture2D, texture)

            ' Draw the cube
            GL.DrawArrays(BeginMode.Triangles, 0, 36)

            ' Draw some text on top of our OpenGL object
            window.PushGLStates()
            window.Draw(text)
            window.PopGLStates()

            ' Finally, display the rendered frame on screen
            window.Display()

        End While

        ' Don't forget to destroy our texture
        GL.DeleteTextures(1, texture)

    End Sub

    ''' <summary>
    ''' Function called when the window is closed
    ''' </summary>
    Sub App_Closed(ByVal sender As Object, ByVal e As EventArgs) Handles window.Closed
        Dim window = CType(sender, RenderWindow)
        window.Close()
    End Sub

    ''' <summary>
    ''' Function called when a key is pressed
    ''' </summary>
    Sub App_KeyPressed(ByVal sender As Object, ByVal e As KeyEventArgs) Handles window.KeyPressed
        Dim window = CType(sender, RenderWindow)
        If e.Code = Keyboard.Key.Escape Then
            window.Close()
        End If
    End Sub

    ''' <summary>
    ''' Function called when the window is resized
    ''' </summary>
    Sub App_Resized(ByVal sender As Object, ByVal e As SizeEventArgs) Handles window.Resized
        GL.Viewport(0, 0, e.Width, e.Height)
    End Sub

End Module
