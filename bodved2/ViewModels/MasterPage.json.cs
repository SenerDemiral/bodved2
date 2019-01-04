using BDB2;
using Starcounter;
using System;
using System.Linq;

namespace bodved2.ViewModels
{
    partial class MasterPage : Json
    {
        [MasterPage_json.Sgn]
        partial class SgnPartial : Json
        {

            void Handle(Input.CancelT Action)
            {
                IsOpened = false;
            }

            void Handle(Input.SignT Action)
            {
                UU uu = null;
                var p = this.Parent as MasterPage;

                if (Action.Value < 0)   // AutoSign In/Out
                {
                    if (!string.IsNullOrEmpty(Token))  // AutoSignIn
                    {
                        uu = Db.SQL<UU>("select r from UU r where r.Token = ?", Token).FirstOrDefault();
                        if (uu == null)
                        {
                            Token = "";
                            p.Token = "";
                            Mesaj = "Hatali Token";
                        }
                        else
                        {
                            p.Token = Token;
                            Mesaj = "Signed";
                        }
                    }
                    else  // AutoSignOut
                    {
                        p.Token = "";
                        Mesaj = "UnSigned";
                    }
                }
                else
                {
                    if (IsOpened)   // SignIn
                    {
                        if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Pwd))
                        {
                            uu = Db.SQL<UU>("select r from UU r where r.Email = ?", Email).FirstOrDefault();
                            if (uu != null)  // SignIn
                            {
                                if (uu.Pwd == Pwd)
                                {
                                    Pwd = "";
                                    Token = uu.Token;
                                    p.Token = uu.Token;
                                    Mesaj = "SignedIn";
                                    IsOpened = false;
                                }
                                else
                                {
                                    Pwd = "";
                                    Token = "";
                                    p.Token = "";
                                    Mesaj = "Hatali eMail/Password";
                                }
                            }
                            else   // SignUp
                            {
                                var newToken = H.EncodeQueryString(Email); // CreateToken
                                Db.Transact(() =>
                                {
                                    new UU
                                    {
                                        Email = Email,
                                        Pwd = Pwd,
                                        Token = newToken,
                                        InsTS = DateTime.Now,
                                        IsConfirmed = false,
                                    };
                                });
                                var email = H.EncodeQueryString(Email);
                                H.SendMail(email);
                                Email = "";
                                Pwd = "";
                                Token = "";
                                Mesaj = "Mailinize gelen linki týklayarak doðrulama iþlemini tamamlayýn.";
                            }
                        }
                    }
                    else   // SignOut / SignIn Request
                    {
                        if (!string.IsNullOrEmpty(Token))  // SignOut
                        {
                            Token = "";
                            p.Token = "";
                            Mesaj = "SignedOut";
                        }
                        else   // SignIn Request
                        {
                            IsOpened = true;
                        }
                    }
                }
            }

        }
    }
}
