namespace MusicCenterAPI.Pages
{
    public class audio
    {
        public static string audioPageHtml = @"
<div class='main'>
    <div class='main-content'>
        <div class='slide-info'>
            <div class='cover-photo-container'>
                <img id='record-cover-photo' src=%coverphoto%/>
                <div class='cover-photo-overlay'></div>
            </div>
            <div class='header-info'>
                <img id='poster-record' src=%poster%/>
                <div class='record-info'>
                    <br />
                    <br />
                    <br />
                    <span id='record-title'>%displayName%</span>
                    <br />
                    <div class='record-sublinetext'>
                        <span id='record-stageName'>Nghệ Sĩ: %artistName%</span>
                        |
                        <span id='record-views-display'>Lượt nghe: %views%</span>
                    </div>
                    
                </div>
            </div>
        </div>
        <div class='control-audio'>
            <div class='audio-player'>
                <div class='timeline'>
                    <div class='progress'></div>
                </div>
                <div class='controls'>
                    <div class='play-container'>
                        <div class='toggle-play pause'>
                        </div>
                    </div>                   
                    <div id='record-name'>Music Song</div>
                    <div class='volume-container'>
                        <div class='volume-button'>
                            <img src='/lib/audio-max-svgrepo-com.svg' />
                        </div>

                        <div class='volume-slider'>
                            <div class='volume-percentage'></div>
                        </div>
                    </div>
                    <div class='time'>
                        <div class='current'>0:00</div>
                        <div class='divider'>/</div>
                        <div class='length'></div>
                    </div>
                </div>
            </div>
            <div class='interact-audio'>
                <div class='artist-region'>
                    <div class='artist-avata-auido'>
                        <img id='img-avata-artist-auido' src=%artist_avata%/>
                    </div>
                    <div class='info-artist-audio'>
                        <span id='title-artist-audio' style='color:white; font-size: 17px;'>%artistName%</span><br />
                        <span id='total-listen-audio' style='color: gray; font-size: 14px'>Tổng lượt nghe: %totalListen%</span>
                    </div>
                </div>
                <div class='favourite-control'>
                    <div class='favourite-button'>
                        <span style='width: 50px; height: 50px; color: white; margin: auto; font-size: 30px; line-height: 60px'>❤</span>
                        <span id='favourite-total' style='color: rgb(127, 127, 127); margin:auto; font-size:15px'>0</span>

                    </div>
                </div>
                <div class='playlist-control'>

                </div>
            </div>
        </div>
    </div>
</div>";

        public static string script = @"

document.addEventListener('DOMContentLoaded', function () {
    const uri = 'https://192.168.0.106/api/';
    const uidRecord = '%recordUid%';

    fetch(uri + 'Record/' + uidRecord)
        .then(response => response.json())
        .then(data => {
            const record = data;

            fetch(uri + 'Artist/' + record.artistUid)
                .then(artistResponse => artistResponse.json())
                .then(artist => {
                    document.getElementById('poster-record').src = 'data:image/jpeg;base64,' + record.poster;
                    document.getElementById('record-cover-photo').src = 'data:image/jpeg;base64,' + record.coverPhoto;
                    document.getElementById('record-title').textContent = record.displayName;
                    document.getElementById('record-stageName').textContent = 'Nghệ Sĩ: ' + artist.stageName;
                    document.getElementById('record-views-display').textContent = 'Lượt nghe: ' + record.views;
                    document.getElementById('record-name').textContent = record.displayName;
                    document.getElementById('total-listen-audio').textContent = 'Tổng lượt nghe: ';
                    document.getElementById('title-artist-audio').textContent = artist.stageName;
                    document.getElementById('img-avata-artist-auido').src = 'data:image/jpeg;base64,' + artist.avata;

                    const audioPlayer = document.querySelector('.audio-player');
                    const audio = new Audio('data:audio/mp3;base64,' + record.record);
                    audio.play();

                    audio.addEventListener('loadeddata', () => {
                        audioPlayer.querySelector('.time .length').textContent = getTimeCodeFromNum(audio.duration);
                        audio.volume = 0.75;
                        
                        fetch(uri + `Record/AddViews/` + uidRecord + `?amount=1`, {
                            method: 'PUT',
                            headers: {
                                'Content-Type': 'application/json',
                            }
                        })
                            .then(response => {
                                if (!response.ok) {
                                    console.error('Error updating views');
                                }
                            })
                            .catch(error => console.error('Error:', error));
                    });

                    // Click on timeline to skip around
                    const timeline = audioPlayer.querySelector('.timeline');
                    timeline.addEventListener('click', e => {
                        const timelineWidth = window.getComputedStyle(timeline).width;
                        const timeToSeek = e.offsetX / parseInt(timelineWidth) * audio.duration;
                        audio.currentTime = timeToSeek;
                    });

                    // Click volume slider to change volume
                    const volumeSlider = audioPlayer.querySelector('.controls .volume-slider');
                    volumeSlider.addEventListener('click', e => {
                        const sliderWidth = window.getComputedStyle(volumeSlider).width;
                        const newVolume = e.offsetX / parseInt(sliderWidth);
                        audio.volume = newVolume;
                        audioPlayer.querySelector('.controls .volume-percentage').style.width = newVolume * 100 + '%';
                    });

                    // Check audio percentage and update time accordingly
                    setInterval(() => {
                        const progressBar = audioPlayer.querySelector('.progress');
                        progressBar.style.width = (audio.currentTime / audio.duration) * 100 + '%';
                        audioPlayer.querySelector('.time .current').textContent = getTimeCodeFromNum(audio.currentTime);
                    }, 500);

                    // Toggle between playing and pausing on button click
                    const playBtn = audioPlayer.querySelector('.controls .toggle-play');
                    playBtn.addEventListener('click', () => {
                        if (audio.paused) {
                            playBtn.classList.remove('play');
                            playBtn.classList.add('pause');
                            audio.play();
                        } else {
                            playBtn.classList.remove('pause');
                            playBtn.classList.add('play');
                            audio.pause();
                        }
                    });

                    audioPlayer.querySelector('.volume-button').addEventListener('click', () => {
                        const volumeEl = audioPlayer.querySelector('.volume-container .volume-button');
                        audio.muted = !audio.muted;
                        volumeEl.innerHTML = ''; // Clear existing content
                        const img = document.createElement('img');
                        img.src = audio.muted ? '/lib/speaker-0-svgrepo-com.svg' : '/lib/audio-max-svgrepo-com.svg';
                        volumeEl.appendChild(img);
                    });
                })
                .catch(error => console.error('Error:', error));
        })
        .catch(error => console.error('Error:', error));
});

// Turn 128 seconds into 2:08
function getTimeCodeFromNum(num) {
    let seconds = parseInt(num);
    let minutes = parseInt(seconds / 60);
    seconds -= minutes * 60;
    const hours = parseInt(minutes / 60);
    minutes -= hours * 60;

    if (hours === 0) return `${minutes}:${String(seconds % 60).padStart(2, 0)}`;
    return `${String(hours).padStart(2, 0)}:${minutes}:${String(seconds % 60).padStart(2, 0)}`;
}

";
    }
}