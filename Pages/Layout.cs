namespace MusicCenterAPI.Pages
{
    public class Layout
    {
        public static string layout = @"<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='utf-8' />
    <meta name='viewport' content='width=device-width, initial-scale=1.0' />
    <title>%title%</title>
    <link rel='preconnect' href='https://fonts.googleapis.com'>
    <link rel='preconnect' href='https://fonts.gstatic.com' crossorigin>
    <link href='https://fonts.googleapis.com/css2?family=Exo:ital,wght@0,100..900;1,100..900&family=Playwrite+ES+Deco:wght@100..400&display=swap' rel='stylesheet'>
</head>
<style>
.container {
    width: 100%;
    height: 100vh;
}

.pb-3 {
    width: 100%;
    height: 100%;
    display: flex;
}

body {
    background-color: rgb(44, 38, 34);
    width: 100%;
    height: 100%;
    overflow: hidden;
    font-family: ""Exo"", sans-serif;
}

.header {
    width: 100%;
    height: 60px;
    background-color: rgb(44, 38, 34);
    padding: 5px;
    display: flex;
    z-index: 10;
}

    .header .logo {
        width: 30%;
        height: 55px;
    }

        .header .logo img {
            width: 55px;
            height: 55px;
        }

.header-control {
    width: 70%;
    height: 100%;
    display: flex;
}

    .header-control #search {
        width: 50%;
        height: 100%;
        border-radius: 100px;
        background-color: rgb(156, 156, 156);
        color: rgb(187, 187, 187);
        border: 3px solid rgb(81, 81, 81);
        font-size: 16px;
    }

        .header-control #search:focus {
            outline: none;
            border: 3px solid rgb(247, 110, 13);
        }

.header .account {
    width: 20%;
    height: 80%;
    margin: auto;
    display: flex;
}

    .header .account #signup {
        background-color: transparent;
        color: rgb(133, 118, 98);
    }

.account-control {
    width: 35%;
    height: 95%;
    margin: auto 10px;
    display: flex;
    font-size: 17px;
    font-weight: bold;
    border-radius: 100px;
    border: none;
    outline: none;
    cursor: pointer;
}

    .account-control:hover {
        background-color: rgb(239, 107, 41);
        color: rgb(255, 255, 255);
    }

.header .account #signup:hover {
    background-color: rgb(239, 107, 41);
    color: rgb(255, 255, 255);
}

.control-btn {
    width: 50px;
    height: 50px;
    color: #a1a1a1;
    font-size: 18px;
    border-radius: 1em;
    background: #975d2b;
    cursor: pointer;
    border: 1px solid #975d2b;
    transition: all 0.3s;
    box-shadow: 6px 6px 12px #483124, -6px -6px 12px #623e29;
    margin: auto 5px;
}

    .control-btn svg {
        fill: white;
    }

    .control-btn:active {
        color: #dedede;
        box-shadow: inset 4px 4px 12px #483124, inset -4px -4px 12px #623e29;
    }

.main {
    display: flex;
    width: 74%;
    height: 88vh;
    float: right;
    background: linear-gradient(rgb(54, 54, 54), rgb(104, 85, 74));
    border-radius: 10px;
    margin: 5px 5px;
}

.leftside {
    float: left;
    width: 25%;
    height: 88vh;
    background-color: #352f2e;
    border-radius: 10px;
    margin: 5px 5px;
}

.header-leftside {
    width: 100%;
    height: 10%;
    margin: auto;
    background-color: #4a4746;
    display: flex;
    border-radius: 10px;
}

.leftside-title {
    width: 70%;
    margin: auto;
    float: left;
}

#add-playlist {
    width: 30%;
    height: 100%;
    border: none;
    outline: none;
    float: right;
    cursor: pointer;
    background-color: #4a4746;
    color: #a1a1a1;
    font-size: 26px;
    font-weight: bold;
    border-radius: 10px;
}

    #add-playlist:hover {
        color: #f8f8f8;
        background-color: #d56b0d;
    }

.leftside-content {
    width: 100%;
    height: 70%;
}

.leftside-footer .content {
    width: 100%;
    height: 80%;
}

.leftside-footer {
    color: #a7a7a7;
    width: 100%;
    height: 20%;
    background-color: #3d3938;
    border-radius: 10px;
}

    .leftside-footer .copyright {
        text-align: center;
        width: 100%;
        height: 20%;
    }

.h9hr8347nt {
    color: rgb(146, 146, 146);
    font-size: 13px;
    line-height: 50px;
    margin: 10px;
    cursor: pointer;
    text-decoration: none;
}

.un834rfniub {
    width: 90%;
}

.h34hn8cth3 {
    width: 10%;
}

.link-card {
    color: rgb(144, 142, 140);
    font-size: 20px;
    line-height: 30px;
    margin-left: 20px;
    cursor: pointer;
    text-decoration: none;
}

.showall-link {
    color: rgb(146, 128, 119);
    font-size: 17px;
    line-height: 30px;
    cursor: pointer;
    text-decoration: underline;
}

.link-card:hover {
    color: rgb(206, 148, 91);
}

.main-content {
    float: right;
    width: 100%;
    min-height: 99%;
    border-radius: 10px;
    z-index: 0;
    display: block;
    margin: 5px 5px;
    overflow-y: auto;
    overflow-block: auto;
}

    .main-content > .type-card {
        min-width: 99%;
        margin: 10px auto;
        display: block;
        min-height: 49%;
    }

.title-card-main-content {
    width: 100%;
    height: 5%;
    display: flex;
    justify-content: space-between;
}

div::-webkit-scrollbar {
    background-color: transparent;
}

div::-webkit-scrollbar-thumb {
    background-color: rgba(239, 113, 35, 0.209);
    transition-duration: 0.5s;
    border-radius: 100px;
}

    div::-webkit-scrollbar-thumb:hover {
        background-color: rgb(239, 113, 35);
    }

.type-card-content {
    display: flex;
    overflow-x: auto;
    overflow-y: hidden;
    width: 100%;
    height: 95%;
}

.artist-item {
    width: 170px;
    height: 230px;
    cursor: pointer;
    margin: 30px 5px;
    display: block;
    border-radius: 10px;
}

    .artist-item:hover {
        background-color: rgb(120, 66, 42);
        transition-duration: 0.2s;
    }

.record-item {
    width: 170px;
    height: 230px;
    cursor: pointer;
    margin: auto 5px;
    display: block;
    border-radius: 10px;
    white-space: nowrap;
    overflow: hidden;
    text-overflow: ellipsis;
    color: white;
}

    .record-item:hover {
        background-color: rgb(120, 66, 42);
        transition-duration: 0.2s;
    }
.avata-container{
    margin:10px auto;
    width:130px;
    height:130px;
}
.title-item {
    color: white;
    font-size: 18px;
    text-decoration: none;
    margin: auto 10px;
}

.subline-item {
    color: rgb(172, 172, 172);
    text-decoration: none;
    font-size: 15px;
    margin: auto 10px;
}

.artist-avt-item {
    width: 100%;
    height: 100%;
    background-color: rgb(85, 85, 85);
    margin: 10px auto;
    border-radius: 100%;
    object-fit: contain;
    box-shadow: 0px 0px 50px rgb(33, 33, 33);
}

.record-poster-item {
    width: 135px;
    height: 135px;
    background-color: rgb(85, 85, 85);
    margin: 10px 10%;
    border-radius: 5px;
    object-fit: contain;
    box-shadow: 0px 0px 50px rgb(33, 33, 33);
}

.artist-avt-item:hover {
    transition-duration: 0.5s;
    transform: scale(1.1);
}

#top-page-card {
    overflow: hidden;
}

    #top-page-card > .artist-item {
        position: relative;
        width: 270px;
        margin: 50px 10px
    }




    #all-content-card > .type-card-content {
        display: flex;
        flex-wrap: wrap;
    }
#page-control {
    width: 100%;
    height: 10%;
    margin: 10px auto;
    display: flex;
    overflow: hidden;
}
#page-control > .page-control-btn {
    width: 45%;
    height: 100%;
    margin: auto 5%;
    background-color: rgb(172, 172, 172, 0.1);
    color:white;
    border-radius:10px;
    font-size:20px;
    cursor:pointer;
}
    #page-control > .page-control-btn div {
        margin: 10px auto;
        width: 50%;
        height:50%;
    }
/*LoaderEffect*/
/* From Uiverse.io by mobinkakei */
.wrapper {
    width: 200px;
    height: 60px;
    position: relative;
    z-index: 1;
    margin: auto;
}

.circle {
    width: 20px;
    height: 20px;
    position: absolute;
    border-radius: 50%;
    background-color: #fff;
    left: 15%;
    transform-origin: 50%;
    animation: circle7124 .5s alternate infinite ease;
}

@keyframes circle7124 {
    0% {
        top: 60px;
        height: 5px;
        border-radius: 50px 50px 25px 25px;
        transform: scaleX(1.7);
    }

    40% {
        height: 20px;
        border-radius: 50%;
        transform: scaleX(1);
    }

    100% {
        top: 0%;
    }
}

.circle:nth-child(2) {
    left: 45%;
    animation-delay: .2s;
}

.circle:nth-child(3) {
    left: auto;
    right: 15%;
    animation-delay: .3s;
}

.shadow {
    width: 20px;
    height: 4px;
    border-radius: 50%;
    background-color: rgba(0,0,0,0.9);
    position: absolute;
    top: 62px;
    transform-origin: 50%;
    z-index: -1;
    left: 15%;
    filter: blur(1px);
    animation: shadow046 .5s alternate infinite ease;
}

@keyframes shadow046 {
    0% {
        transform: scaleX(1.5);
    }

    40% {
        transform: scaleX(1);
        opacity: .7;
    }

    100% {
        transform: scaleX(.2);
        opacity: .4;
    }
}

.shadow:nth-child(4) {
    left: 45%;
    animation-delay: .2s
}

.shadow:nth-child(5) {
    left: auto;
    right: 15%;
    animation-delay: .3s;
}
.slide-info {
    width: 100%;
    height: 65%;
    background: linear-gradient(rgb(255, 255, 255, 0,00), rgb(20, 20, 20, 0,82));
    position: relative;
}

.cover-photo-container {
    width: 100%;
    height: 100%;
    position: relative;
}

.cover-photo-overlay {
    position: absolute;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    background: linear-gradient(to bottom, rgba(255, 255, 255, 0.0), rgba(0, 0, 0, 100));
    border-radius: 10px;
}

.header-info {
    position: absolute;
    width: 100%;
    height: 50%;
    bottom: 0px;
    display: flex;
}

#record-cover-photo {
    width: 100%;
    height: 100%;
    object-fit: contain;
}

#poster-record {
    left: 10px;
    width: 190px;
    height: 190px;
    object-fit: contain;
    z-index: 100;
    border-radius: 15px;
    border: 4px solid white;
    box-shadow: 0px 0px 50px black;
}

.record-info {
    width: 100%;
    height: 100%;
    display: block;
}

#record-title {
    color: white;
    font-size: 30px;
    margin-left: 20px;
}

.record-sublinetext {
    height: 60px;
    color: rgb(163, 163, 163);
    font-size: 15px;
    margin-left: 20px;
    display: flex;
}

    .record-sublinetext span {
        margin: 0px 10px;
    }

.control-audio {
    margin-top: 20px;
    width: 100%;
    height: 30%;
    background-color: rgb(47, 47, 47);
    border-radius: 10px;
}

    .control-audio audio {
        margin: 10px 5px;
        width: 70%;
    }

.audio-player {
    height: 70px;
    width: 100%;
    font-family: arial;
    color: white;
    font-size: 0.75em;
    overflow: hidden;
    display: grid;
    grid-template-rows: 6px auto;
    .timeline

{
    background: white;
    width: 100%;
    position: relative;
    cursor: pointer;
    box-shadow: 0 2px 10px 0 #0008;
    .progress

{
    background: coral;
    width: 0%;
    height: 100%;
    transition: 0.25s;
}

}

.controls {
    display: flex;
    justify-content: space-between;
    align-items: stretch;
    padding: 0 20px;
    position: relative;
    > *

{
    display: flex;
    justify-content: center;
    align-items: center;
}

.play-container {
    width: 50px;
    height: 50px;
    justify-items: center;
    margin: auto 0px;
    .toggle-play

{
    margin: auto;
    &.play

{
    margin: auto;
    cursor: pointer;
    position: relative;
    left: 0;
    height: 0;
    width: 0;
    border: 13px solid #0000;
    border-left: 24px solid rgb(254, 110, 24);
    &:hover

{
    transform: scale(1.1);
}

}

&.pause {
    margin: auto;
    height: 20px;
    width: 31px;
    cursor: pointer;
    position: relative;
    &:before

{
    margin: auto;
    position: absolute;
    top: 0;
    left: 0px;
    background: rgb(254, 110, 24);
    content: "";
    height: 24px;
    width: 7px;
}

&:after {
    margin: auto;
    position: absolute;
    top: 0;
    right: 8px;
    background: rgb(254, 110, 24);
    content: "";
    height: 24px;
    width: 7px;
}

&:hover {
    transform: scale(1.1);
}

}
}
}


.time {
    display: flex;
    > *

{
    padding: 2px;
}

}

.volume-container {
    cursor: pointer;
    position: relative;
    z-index: 10;
    width: 140px;
    .volume-button

{
    height: 26px;
    width: 26px;
    display: flex;
    &:hover

{
    transform: scale(0.9);
}

}

.volume-slider {
    margin: auto;
    z-index: -1;
    height: 10px;
    background: rgb(143, 143, 143);
    transition: .25s;
    width: 100px;
    .volume-percentage

{
    background: coral;
    height: 100%;
    width: 75%;
}

}
}
}
}

.volume {
    width: 26px;
    height: 26px;
}

.interact-audio {
    width: 97%;
    height: 55%;
    background-color: rgb(25, 25, 25, 0.4);
    margin: auto;
    border-radius: 10px;
    box-shadow: 0px 8px 10px rgb(70, 70, 70);
    display:flex;
}
.artist-region{
    width: 40%;
    height: 100%;
    display:flex;
    cursor: pointer;
}
.artist-avata-auido {
    width: 90px;
    height: 90px;
    border-radius: 100%;
    border: 4px solid rgb(228, 107, 78);
    margin: auto 10px;
    transition-duration: 0.2s;
}
    .artist-avata-auido img {
        width: 100%;
        height: 100%;
        object-fit: contain;
        border-radius: 100%;
        border: none;
    }
.artist-region:hover > .artist-avata-auido {
    transform: scale(1.2);
}
.info-artist-audio{
    width: auto;
    height: 80%;
    margin:auto 10px;
}
.info-artist-audio span{
    margin: auto;
}
.favourite-control {
    width: 30%;
    height: 100%;
    display: block;
    .favourite-button
{
    cursor: pointer;
    width: 50%;
    height: 100%;
    margin: auto 20px;
    & img
{
    margin: 20px auto;
    width: 40px;
    height: 40px;
}
}
}


</style>
<script>%script%</script>
<body>
    <header>
        <div class='header'>
            <div class='logo'><img src='~/lib/logo.png' alt=''></div>
            <div class='header-control'>
                <button class='control-btn' id='farvorite'>
                    <svg width='30' height='30' viewBox='0 0 200 200' xmlns='http://www.w3.org/2000/svg'>
                        <polygon points='100,10 120,75 190,75 130,115 150,180 100,140 50,180 70,115 10,75 80,75' fill='white' />
                    </svg>
                </button>
                <button class='control-btn' id='browser'>
                    <svg class='category-icon' xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24'>
                        <rect x='3' y='6' width='2' height='2' />
                        <rect x='3' y='10' width='2' height='2' />
                        <rect x='3' y='14' width='2' height='2' />
                        <rect x='7' y='6' width='14' height='2' />
                        <rect x='7' y='10' width='14' height='2' />
                        <rect x='7' y='14' width='14' height='2' />
                    </svg>
                </button>

                <input type='text' name='' placeholder='What do you want to play?' id='search'>
            </div>
            <div class='account'>
                <input class='account-control' type='button' name='' id='signup' value='Sign up'>
                <input class='account-control' type='button' name='' id='signin' value='Log in'>
            </div>
        </div>
    </header>
    <div class='container'>        
        <main role='main' class='pb-3'>
            <div class='leftside'>
                <div class='header-leftside'>
                    <div class='leftside-title'>
                        <a href='#' style='color: rgb(146, 146, 146); font-size: 19px; line-height: 50px; margin-left: 15px; cursor: pointer; text-decoration: none;'>
                            ▶ PlayList
                        </a>
                    </div>
                    <button id='add-playlist'>+</button>
                </div>
                <div class='leftside-content'></div>
                <div class='leftside-footer'>
                    <div class='leftside-footer content'>
                        <a class='h9hr8347nt' href='#'>Điều khoản</a> <a class='h9hr8347nt' href='#'>Hướng dẫn</a>
                    </div>
                    <div class='leftside-footer copyright'>
                        &copy DoThanhTung - 2024
                    </div>
                </div>
            </div>
            %page%
        </main>
    </div>
</body>
</html>";
    }
}
