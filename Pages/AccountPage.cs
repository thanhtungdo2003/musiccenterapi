﻿namespace MusicCenterAPI.Pages
{
    public class AccountPage
    {
        public static string login = @" <div class=""form-account"">
                <span class=""title"">ĐĂNG NHẬP</span>
                <hr/>
                <span id=""mess"" style=""color: red; font-size: 15px;""></span>
                <input type=""text"" placeholder=""example@gmail.com"" class=""textbox"" name="""" id=""username""/><br>
                <input type=""password"" placeholder=""Mật khẩu"" class=""textbox"" name="""" id=""password""/><br>
                <div class=""action-ac"">
                    <div>
                        <input type=""checkbox"" name="""" id=""forgot"" class=""forgot-pass"" ><label for=""forgot"" style=""font-size: 15px; color: rgb(121, 121, 121);"">Nhớ mật khẩu</label>
                    </div>
                    <a href=""#"">Quên mật khẩu</a>
                </div>
                <input type=""button"" name="""" value=""ENTER"" id=""sumbit""><br>
                <hr/>
                <span style=""color: rgb(138, 138, 138); font-size: 13px;"">Hoặc đăng nhập với</span><br>
                <div class=""other-login"">
                    <button class=""signin-gg"">
                        <svg
                          viewBox=""0 0 256 262""
                          preserveAspectRatio=""xMidYMid""
                          xmlns=""http://www.w3.org/2000/svg""
                        >
                          <path
                            d=""M255.878 133.451c0-10.734-.871-18.567-2.756-26.69H130.55v48.448h71.947c-1.45 12.04-9.283 30.172-26.69 42.356l-.244 1.622 38.755 30.023 2.685.268c24.659-22.774 38.875-56.282 38.875-96.027""
                            fill=""#4285F4""
                          ></path>
                          <path
                            d=""M130.55 261.1c35.248 0 64.839-11.605 86.453-31.622l-41.196-31.913c-11.024 7.688-25.82 13.055-45.257 13.055-34.523 0-63.824-22.773-74.269-54.25l-1.531.13-40.298 31.187-.527 1.465C35.393 231.798 79.49 261.1 130.55 261.1""
                            fill=""#34A853""
                          ></path>
                          <path
                            d=""M56.281 156.37c-2.756-8.123-4.351-16.827-4.351-25.82 0-8.994 1.595-17.697 4.206-25.82l-.073-1.73L15.26 71.312l-1.335.635C5.077 89.644 0 109.517 0 130.55s5.077 40.905 13.925 58.602l42.356-32.782""
                            fill=""#FBBC05""
                          ></path>
                          <path
                            d=""M130.55 50.479c24.514 0 41.05 10.589 50.479 19.438l36.844-35.974C195.245 12.91 165.798 0 130.55 0 79.49 0 35.393 29.301 13.925 71.947l42.211 32.783c10.59-31.477 39.891-54.251 74.414-54.251""
                            fill=""#EB4335""
                          ></path>
                        </svg>
                             Đăng nhập với Google
                    </button>
                    <button class=""signin-gg"">
                        <svg viewBox=""0 0 16 16"" class=""bi bi-facebook"" fill=""currentColor"" height=""16"" width=""16"">
                            <path d=""M16 8.049c0-4.446-3.582-8.05-8-8.05C3.58 0-.002 3.603-.002 8.05c0 4.017 2.926 7.347 6.75 7.951v-5.625h-2.03V8.05H6.75V6.275c0-2.017 1.195-3.131 3.022-3.131.876 0 1.791.157 1.791.157v1.98h-1.009c-.993 0-1.303.621-1.303 1.258v1.51h2.218l-.354 2.326H9.25V16c3.824-.604 6.75-3.934 6.75-7.951z"" style=""fill: rgb(0, 129, 228);""></path>
                        </svg>
                             Đăng nhập với Facbook
                      </button>
                </div><br>
                <hr/>
                <div class=""question""><span>Chưa có tài khoản? </span><a href=""signin.php"">Đăng ký</a></div>
           </div>";
    }
}
