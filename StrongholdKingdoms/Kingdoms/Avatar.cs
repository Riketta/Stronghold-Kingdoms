namespace Kingdoms
{
    using CommonTypes;
    using DXGraphics;
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Runtime.InteropServices;

    public class Avatar
    {
        public static Bitmap CreateAvatar(AvatarData avatarData, Bitmap bitmap)
        {
            return CreateAvatar(avatarData, bitmap, Color.FromArgb(0x69, 0x77, 0x81), true);
        }

        public static Bitmap CreateAvatar(AvatarData avatar, Color backgroundColour)
        {
            Bitmap bitmap = new Bitmap(0x9a, 500);
            return CreateAvatar(avatar, bitmap, backgroundColour);
        }

        public static Bitmap CreateAvatar(AvatarData avatar, int height)
        {
            Bitmap bitmap = new Bitmap(0x9a, 500);
            CreateAvatar(avatar, bitmap, ARGBColors.LightGoldenrodYellow, true);
            Bitmap image = new Bitmap((0x9a * height) / 500, height);
            Graphics graphics = Graphics.FromImage(image);
            graphics.DrawImage(bitmap, new Rectangle(0, 0, image.Width, image.Height), new Rectangle(0, 0, 0x9a, 500), GraphicsUnit.Pixel);
            graphics.Dispose();
            bitmap.Dispose();
            return image;
        }

        public static Bitmap CreateAvatar(AvatarData avatarData, Bitmap bitmap, Color backgroundColour)
        {
            return CreateAvatar(avatarData, bitmap, backgroundColour, true);
        }

        public static Bitmap CreateAvatar(AvatarData avatarData, Bitmap bitmap, Color backgroundColour, bool drawParchment)
        {
            if (GFXLibrary.avatar_parchment_base_layer != null)
            {
                Graphics graphics = Graphics.FromImage(bitmap);
                Rectangle rect = new Rectangle(0, 0, 0x9a, 500);
                SolidBrush brush = new SolidBrush(backgroundColour);
                graphics.FillRectangle(brush, rect);
                brush.Dispose();
                if (drawParchment)
                {
                    graphics.DrawImage((Image) GFXLibrary.avatar_parchment_base_layer, rect, rect, GraphicsUnit.Pixel);
                }
                ShrunkImage image = null;
                Color white = ARGBColors.White;
                white = avatarData.ShouldersColour;
                image = getRear(avatarData);
                if (image != null)
                {
                    graphics.DrawImage(image.image, image.Dest, image.Source.X, image.Source.Y, image.Source.Width, image.Source.Height, GraphicsUnit.Pixel, createColour(white));
                }
                white = ARGBColors.White;
                image = getFloor(avatarData);
                if (image != null)
                {
                    graphics.DrawImage(image.image, image.Dest, image.Source.X, image.Source.Y, image.Source.Width, image.Source.Height, GraphicsUnit.Pixel, createColour(white));
                }
                white = avatarData.BodyColour;
                image = getBody(avatarData);
                if (image != null)
                {
                    graphics.DrawImage(image.image, image.Dest, image.Source.X, image.Source.Y, image.Source.Width, image.Source.Height, GraphicsUnit.Pixel, createColour(white));
                }
                if (((avatarData.legs != 4) && (avatarData.legs != 5)) && ((avatarData.legs != 6) && (avatarData.feet != 4)))
                {
                    white = avatarData.LegsColour;
                    image = getLegs(avatarData);
                    if (image != null)
                    {
                        graphics.DrawImage(image.image, image.Dest, image.Source.X, image.Source.Y, image.Source.Width, image.Source.Height, GraphicsUnit.Pixel, createColour(white));
                    }
                    white = avatarData.FeetColour;
                    image = getFeet(avatarData);
                    if (image != null)
                    {
                        graphics.DrawImage(image.image, image.Dest, image.Source.X, image.Source.Y, image.Source.Width, image.Source.Height, GraphicsUnit.Pixel, createColour(white));
                    }
                }
                else
                {
                    white = avatarData.FeetColour;
                    image = getFeet(avatarData);
                    if (image != null)
                    {
                        graphics.DrawImage(image.image, image.Dest, image.Source.X, image.Source.Y, image.Source.Width, image.Source.Height, GraphicsUnit.Pixel, createColour(white));
                    }
                    white = avatarData.LegsColour;
                    image = getLegs(avatarData);
                    if (image != null)
                    {
                        graphics.DrawImage(image.image, image.Dest, image.Source.X, image.Source.Y, image.Source.Width, image.Source.Height, GraphicsUnit.Pixel, createColour(white));
                    }
                }
                white = avatarData.TorsoColour;
                image = getTorso(avatarData);
                if (image != null)
                {
                    graphics.DrawImage(image.image, image.Dest, image.Source.X, image.Source.Y, image.Source.Width, image.Source.Height, GraphicsUnit.Pixel, createColour(white));
                }
                white = avatarData.TabardColour;
                image = getTabard(avatarData);
                if (image != null)
                {
                    graphics.DrawImage(image.image, image.Dest, image.Source.X, image.Source.Y, image.Source.Width, image.Source.Height, GraphicsUnit.Pixel, createColour(white));
                }
                white = avatarData.ArmsColour;
                image = getArms(avatarData);
                if (image != null)
                {
                    graphics.DrawImage(image.image, image.Dest, image.Source.X, image.Source.Y, image.Source.Width, image.Source.Height, GraphicsUnit.Pixel, createColour(white));
                }
                white = avatarData.HandsColour;
                image = getHands(avatarData);
                if (image != null)
                {
                    graphics.DrawImage(image.image, image.Dest, image.Source.X, image.Source.Y, image.Source.Width, image.Source.Height, GraphicsUnit.Pixel, createColour(white));
                }
                white = avatarData.ShouldersColour;
                image = getShoulders(avatarData);
                if (image != null)
                {
                    graphics.DrawImage(image.image, image.Dest, image.Source.X, image.Source.Y, image.Source.Width, image.Source.Height, GraphicsUnit.Pixel, createColour(white));
                }
                white = avatarData.HairColour;
                image = getFace(avatarData);
                if (image != null)
                {
                    graphics.DrawImage(image.image, image.Dest, image.Source.X, image.Source.Y, image.Source.Width, image.Source.Height, GraphicsUnit.Pixel, createColour(white));
                }
                white = avatarData.HairColour;
                image = getHair(avatarData);
                if (image != null)
                {
                    graphics.DrawImage(image.image, image.Dest, image.Source.X, image.Source.Y, image.Source.Width, image.Source.Height, GraphicsUnit.Pixel, createColour(white));
                }
                white = avatarData.HeadColour;
                image = getHead(avatarData);
                if (image != null)
                {
                    graphics.DrawImage(image.image, image.Dest, image.Source.X, image.Source.Y, image.Source.Width, image.Source.Height, GraphicsUnit.Pixel, createColour(white));
                }
                white = avatarData.WeaponColour;
                image = getWeapon(avatarData);
                if (image != null)
                {
                    graphics.DrawImage(image.image, image.Dest, image.Source.X, image.Source.Y, image.Source.Width, image.Source.Height, GraphicsUnit.Pixel, createColour(white));
                }
                white = avatarData.WeaponColour;
                image = getBelt(avatarData);
                if (image != null)
                {
                    graphics.DrawImage(image.image, image.Dest, image.Source.X, image.Source.Y, image.Source.Width, image.Source.Height, GraphicsUnit.Pixel, createColour(white));
                }
                if (true)
                {
                    int length = (bitmap.Width * bitmap.Height) * 4;
                    byte[] destination = new byte[length];
                    Rectangle rectangle2 = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
                    BitmapData bitmapdata = bitmap.LockBits(rectangle2, ImageLockMode.ReadOnly, bitmap.PixelFormat);
                    IntPtr source = bitmapdata.Scan0;
                    Marshal.Copy(source, destination, 0, length);
                    byte[] parchementOverlay = GFXLibrary.parchementOverlay;
                    for (int i = 0; i < parchementOverlay.Length; i += 4)
                    {
                        if (parchementOverlay[i] < 0xff)
                        {
                            destination[i] = (byte) ((destination[i] * parchementOverlay[i]) / 0xff);
                        }
                        if (parchementOverlay[i + 1] < 0xff)
                        {
                            destination[i + 1] = (byte) ((destination[i + 1] * parchementOverlay[i + 1]) / 0xff);
                        }
                        if (parchementOverlay[i + 2] < 0xff)
                        {
                            destination[i + 2] = (byte) ((destination[i + 2] * parchementOverlay[i + 2]) / 0xff);
                        }
                        byte num1 = parchementOverlay[i + 3];
                    }
                    Marshal.Copy(destination, 0, source, length);
                    bitmap.UnlockBits(bitmapdata);
                }
                graphics.Dispose();
            }
            return bitmap;
        }

        public static Bitmap CreateAvatar(AvatarData avatar, int height, Color backgroundColour, bool drawParchment)
        {
            Bitmap bitmap = new Bitmap(0x9a, 500);
            CreateAvatar(avatar, bitmap, backgroundColour, drawParchment);
            Bitmap image = new Bitmap((0x9a * height) / 500, height);
            Graphics graphics = Graphics.FromImage(image);
            graphics.DrawImage(bitmap, new Rectangle(0, 0, image.Width, image.Height), new Rectangle(0, 0, 0x9a, 500), GraphicsUnit.Pixel);
            graphics.Dispose();
            bitmap.Dispose();
            return image;
        }

        private static ImageAttributes createColour(Color color)
        {
            ColorMatrix newColorMatrix = new ColorMatrix {
                Matrix00 = ((float) color.R) / 255f,
                Matrix11 = ((float) color.G) / 255f,
                Matrix22 = ((float) color.B) / 255f,
                Matrix44 = 1f,
                Matrix33 = 1f
            };
            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(newColorMatrix);
            return attributes;
        }

        public static ShrunkImage getArms(AvatarData data)
        {
            switch (data.arms)
            {
                case -1:
                    return null;

                case 1:
                    return (ShrunkImage) GFXLibrary.avatar_arms02;

                case 2:
                    return (ShrunkImage) GFXLibrary.avatar_arms03;

                case 3:
                    return (ShrunkImage) GFXLibrary.avatar_arms04;
            }
            return (ShrunkImage) GFXLibrary.avatar_arms01;
        }

        public static ShrunkImage getBelt(AvatarData data)
        {
            if (data.weapon >= 0)
            {
                return (ShrunkImage) GFXLibrary.avatar_weapon_belt;
            }
            return null;
        }

        public static ShrunkImage getBody(AvatarData data)
        {
            int body = data.body;
            return (ShrunkImage) GFXLibrary.avatar_body01_default;
        }

        public static ShrunkImage getFace(AvatarData data)
        {
            switch (data.face)
            {
                case 1:
                    if (!data.male)
                    {
                        return (ShrunkImage) GFXLibrary.avatar_face04_female;
                    }
                    return (ShrunkImage) GFXLibrary.avatar_face02_male;

                case 2:
                    if (!data.male)
                    {
                        return (ShrunkImage) GFXLibrary.avatar_face05_female;
                    }
                    return (ShrunkImage) GFXLibrary.avatar_face06_male;

                case 3:
                    if (!data.male)
                    {
                        return (ShrunkImage) GFXLibrary.avatar_face06_female;
                    }
                    return (ShrunkImage) GFXLibrary.avatar_face07_male;

                case 4:
                    if (!data.male)
                    {
                        return (ShrunkImage) GFXLibrary.avatar_face08_female;
                    }
                    return (ShrunkImage) GFXLibrary.avatar_face08_male;

                case 5:
                    if (!data.male)
                    {
                        return (ShrunkImage) GFXLibrary.avatar_face09_female;
                    }
                    return (ShrunkImage) GFXLibrary.avatar_face09_male;

                case 6:
                    if (!data.male)
                    {
                        return (ShrunkImage) GFXLibrary.avatar_face10_female;
                    }
                    return (ShrunkImage) GFXLibrary.avatar_face10_male;

                case 7:
                    return (ShrunkImage) GFXLibrary.avatar_rat_face;

                case 8:
                    return (ShrunkImage) GFXLibrary.avatar_snake_face;

                case 9:
                    return (ShrunkImage) GFXLibrary.avatar_pig_face;

                case 10:
                    return (ShrunkImage) GFXLibrary.avatar_wolf_face;
            }
            if (data.male)
            {
                return (ShrunkImage) GFXLibrary.avatar_face01_male;
            }
            return (ShrunkImage) GFXLibrary.avatar_face03_female;
        }

        public static ShrunkImage getFeet(AvatarData data)
        {
            switch (data.feet)
            {
                case -1:
                    return null;

                case 1:
                    return (ShrunkImage) GFXLibrary.avatar_feet02;

                case 2:
                    return (ShrunkImage) GFXLibrary.avatar_feet03;

                case 3:
                    return (ShrunkImage) GFXLibrary.avatar_feet04;

                case 4:
                    return (ShrunkImage) GFXLibrary.avatar_feet05;

                case 5:
                    return (ShrunkImage) GFXLibrary.avatar_feet06;
            }
            return (ShrunkImage) GFXLibrary.avatar_feet01;
        }

        public static ShrunkImage getFloor(AvatarData data)
        {
            switch (data.floor)
            {
                case 1:
                    return (ShrunkImage) GFXLibrary.avatar_floor02;

                case 2:
                    return (ShrunkImage) GFXLibrary.avatar_floor03;

                case 3:
                    return (ShrunkImage) GFXLibrary.avatar_floor04;

                case 4:
                    return (ShrunkImage) GFXLibrary.avatar_floor05;

                case 5:
                    return (ShrunkImage) GFXLibrary.avatar_floor06;

                case 6:
                    return (ShrunkImage) GFXLibrary.avatar_floor07;

                case 7:
                    return (ShrunkImage) GFXLibrary.avatar_floor08;

                case 8:
                    return (ShrunkImage) GFXLibrary.avatar_floor09;

                case 9:
                    return (ShrunkImage) GFXLibrary.avatar_floor10;

                case 10:
                    return (ShrunkImage) GFXLibrary.avatar_floor11;
            }
            return (ShrunkImage) GFXLibrary.avatar_floor01;
        }

        public static ShrunkImage getHair(AvatarData data)
        {
            if (((data.head == 0) || (data.head == 1)) && (data.hair == 0))
            {
                return null;
            }
            switch (data.hair)
            {
                case -1:
                    return null;

                case 1:
                    return (ShrunkImage) GFXLibrary.avatar_hair02;

                case 2:
                    return (ShrunkImage) GFXLibrary.avatar_hair03;

                case 3:
                    return (ShrunkImage) GFXLibrary.avatar_hair04;

                case 4:
                    return (ShrunkImage) GFXLibrary.avatar_hair05;

                case 5:
                    return (ShrunkImage) GFXLibrary.avatar_hair06;
            }
            return (ShrunkImage) GFXLibrary.avatar_hair01_helmhide;
        }

        public static ShrunkImage getHands(AvatarData data)
        {
            switch (data.hands)
            {
                case -1:
                    return null;

                case 1:
                    return (ShrunkImage) GFXLibrary.avatar_hands02;

                case 2:
                    return (ShrunkImage) GFXLibrary.avatar_hands03;

                case 3:
                    return (ShrunkImage) GFXLibrary.avatar_hands04;
            }
            return (ShrunkImage) GFXLibrary.avatar_hands01;
        }

        public static ShrunkImage getHead(AvatarData data)
        {
            switch (data.head)
            {
                case 0:
                    return (ShrunkImage) GFXLibrary.avatar_head01_hairoff;

                case 1:
                    return (ShrunkImage) GFXLibrary.avatar_head02_hairoff;

                case 2:
                    return (ShrunkImage) GFXLibrary.avatar_head03;

                case 3:
                    return (ShrunkImage) GFXLibrary.avatar_head04;

                case 4:
                    return (ShrunkImage) GFXLibrary.avatar_head05;

                case 5:
                    return (ShrunkImage) GFXLibrary.avatar_head06;

                case 6:
                    return (ShrunkImage) GFXLibrary.avatar_head07;

                case 7:
                    return (ShrunkImage) GFXLibrary.avatar_head08;

                case 8:
                    return (ShrunkImage) GFXLibrary.avatar_head09;

                case 9:
                    return (ShrunkImage) GFXLibrary.avatar_head10;

                case 10:
                    return (ShrunkImage) GFXLibrary.avatar_head11;

                case 11:
                    return (ShrunkImage) GFXLibrary.avatar_head12;

                case 12:
                    return (ShrunkImage) GFXLibrary.avatar_rat_helm;

                case 13:
                    return (ShrunkImage) GFXLibrary.avatar_wolf_helm;
            }
            return null;
        }

        public static ShrunkImage getLegs(AvatarData data)
        {
            switch (data.legs)
            {
                case 1:
                    return (ShrunkImage) GFXLibrary.avatar_legs02;

                case 2:
                    return (ShrunkImage) GFXLibrary.avatar_legs03;

                case 3:
                    return (ShrunkImage) GFXLibrary.avatar_legs04;

                case 4:
                    return (ShrunkImage) GFXLibrary.avatar_legs05;

                case 5:
                    return (ShrunkImage) GFXLibrary.avatar_legs06;

                case 6:
                    return (ShrunkImage) GFXLibrary.avatar_legs07;
            }
            if (data.male)
            {
                return (ShrunkImage) GFXLibrary.avatar_legs01_male;
            }
            return (ShrunkImage) GFXLibrary.avatar_legs01_female;
        }

        public static AvatarData getPigAvatar()
        {
            return new AvatarData { 
                male = true, floor = 0, body = 0, legs = 1, feet = 3, torso = 0, tabard = 7, arms = 2, hands = 0, shoulder = 0, face = 9, hair = -1, head = -1, weapon = 2, bodyColour = -2307931, legsColour = -12828346, 
                feetColour = -7896461, torsoColour = -6259366, tabardColour = -10526881, armsColour = -6259366, handsColour = -2633011, shouldersColour = -3619906, hairColour = -10526881, headColour = -2633011, weaponColour = -10529476
             };
        }

        public static AvatarData getRatAvatar()
        {
            return new AvatarData { 
                male = true, floor = 4, body = 0, legs = 2, feet = 2, torso = 3, tabard = -1, arms = 2, hands = 2, shoulder = 1, face = 7, hair = -1, head = 12, weapon = 0, bodyColour = -2307931, legsColour = -4275256, 
                feetColour = -4275256, torsoColour = -4275256, tabardColour = -9217456, armsColour = -2633011, handsColour = -4275256, shouldersColour = -4275256, hairColour = -6588326, headColour = -2633011, weaponColour = -10866131
             };
        }

        public static ShrunkImage getRear(AvatarData data)
        {
            if (data.shoulder != 3)
            {
                return null;
            }
            return (ShrunkImage) GFXLibrary.avatar_shoulders04_back;
        }

        public static ShrunkImage getShoulders(AvatarData data)
        {
            switch (data.shoulder)
            {
                case -1:
                    return null;

                case 1:
                    return (ShrunkImage) GFXLibrary.avatar_shoulders02;

                case 2:
                    return (ShrunkImage) GFXLibrary.avatar_shoulders03;

                case 3:
                    return (ShrunkImage) GFXLibrary.avatar_shoulders04_front;
            }
            return (ShrunkImage) GFXLibrary.avatar_shoulders01;
        }

        public static AvatarData getSnakeAvatar()
        {
            return new AvatarData { 
                male = true, floor = 1, body = 0, legs = 0, feet = 1, torso = 1, tabard = -1, arms = 1, hands = -1, shoulder = -1, face = 8, hair = 0, head = 2, weapon = -1, bodyColour = -2307931, legsColour = -14803426, 
                feetColour = -10526881, torsoColour = -14803426, tabardColour = -9217456, armsColour = -14803426, handsColour = -14803426, shouldersColour = -2633011, hairColour = -13816536, headColour = -9217456, weaponColour = -14803426
             };
        }

        public static ShrunkImage getTabard(AvatarData data)
        {
            switch (data.tabard)
            {
                case -1:
                    return null;

                case 1:
                    return (ShrunkImage) GFXLibrary.avatar_tabard02;

                case 2:
                    return (ShrunkImage) GFXLibrary.avatar_tabard03;

                case 3:
                    return (ShrunkImage) GFXLibrary.avatar_tabard04;

                case 4:
                    return (ShrunkImage) GFXLibrary.avatar_tabard05;

                case 5:
                    return (ShrunkImage) GFXLibrary.avatar_tabard06;

                case 6:
                    return (ShrunkImage) GFXLibrary.avatar_tabard07;

                case 7:
                    return (ShrunkImage) GFXLibrary.avatar_tabard08;
            }
            return (ShrunkImage) GFXLibrary.avatar_tabard01;
        }

        public static ShrunkImage getTorso(AvatarData data)
        {
            switch (data.torso)
            {
                case 1:
                    if (!data.male)
                    {
                        return (ShrunkImage) GFXLibrary.avatar_torso02_female;
                    }
                    return (ShrunkImage) GFXLibrary.avatar_torso02_male;

                case 2:
                    return (ShrunkImage) GFXLibrary.avatar_torso03;

                case 3:
                    return (ShrunkImage) GFXLibrary.avatar_torso04;
            }
            if (data.male)
            {
                return (ShrunkImage) GFXLibrary.avatar_torso01_male_default;
            }
            return (ShrunkImage) GFXLibrary.avatar_torso01_female_default;
        }

        public static ShrunkImage getWeapon(AvatarData data)
        {
            switch (data.weapon)
            {
                case 0:
                    return (ShrunkImage) GFXLibrary.avatar_weapon01;

                case 1:
                    return (ShrunkImage) GFXLibrary.avatar_weapon02;

                case 2:
                    return (ShrunkImage) GFXLibrary.avatar_weapon03;

                case 3:
                    return (ShrunkImage) GFXLibrary.avatar_weapon04;

                case 4:
                    return (ShrunkImage) GFXLibrary.avatar_weapon05;

                case 5:
                    return (ShrunkImage) GFXLibrary.avatar_weapon06;
            }
            return null;
        }

        public static AvatarData getWolfAvatar()
        {
            return new AvatarData { 
                male = true, floor = 5, body = 0, legs = 2, feet = 3, torso = 3, tabard = -1, arms = 2, hands = 2, shoulder = 3, face = 10, hair = -1, head = 13, weapon = 3, bodyColour = -2307931, legsColour = -7896461, 
                feetColour = -12172996, torsoColour = -7896461, tabardColour = -6259366, armsColour = -9217456, handsColour = -12172996, shouldersColour = -10529476, hairColour = -3618626, headColour = -7896461, weaponColour = -10866131
             };
        }
    }
}

