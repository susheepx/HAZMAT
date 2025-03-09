using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


public class ToggleMediaPlayer : MonoBehaviour
{
    public GameObject musicDisplay;
    public AudioSource currentSong;
    public List<AudioClip> songQueue = new List<AudioClip>();
    public int clipNum = 0;
    public Slider seekSlider;
    public Slider volumeSlider;
    public bool isDragging = false;
    public TextMeshProUGUI displayDuration;
    public TextMeshProUGUI songTitle;
    public TextMeshProUGUI songArtist;

    public GameObject songItemPrefab;
    public Transform playlistPanel;

    void Start() {
        // musicDisplay.SetActive(false);
        Time.timeScale = 1;
        PopulatePlaylist();
        currentSong = GetComponent<AudioSource>();
        if (currentSong.clip != null) {
            seekSlider.maxValue = currentSong.clip.length;
        }
        seekSlider.onValueChanged.AddListener(SeekTrack);
        volumeSlider.onValueChanged.AddListener(AdjustVolume);
        songTitle.text = currentSong.clip.name;

    }

    void Update()
    {
        if (!isDragging && currentSong.isPlaying) {
            seekSlider.value = currentSong.time;
        }
        displayDuration.text = $"{FormatTime(seekSlider.value)} / {FormatTime(currentSong.clip.length)}";
    }
    public void Toggle() {
        if (musicDisplay.activeSelf == false) musicDisplay.SetActive(true);
        else musicDisplay.SetActive(false);
    }

    public void PlayLast() {
        clipNum = (clipNum - 1) % songQueue.Count;
        if (clipNum < 0) clipNum = songQueue.Count-1;
        currentSong.clip = songQueue[clipNum];
        PlaySong(songQueue.IndexOf(currentSong.clip));
    }
    public void PlayNext() {
        clipNum = (clipNum + 1) % songQueue.Count;
        currentSong.clip = songQueue[clipNum];
        PlaySong(songQueue.IndexOf(currentSong.clip));
    }
    public void Play() {
        if (currentSong.isPlaying) currentSong.Pause();
        else currentSong.Play();
    }

    public void SeekTrack(float value) {
        if (isDragging) return;
        if (currentSong.clip != null)
        {
            float newTime = Mathf.Clamp(value, 0f, currentSong.clip.length - 0.01f);
            currentSong.Pause(); // Prevents garbled sound
            currentSong.time = newTime;
            currentSong.Play(); // Resumes cleanly from new position
        }   
    }

    public void OnPointerDown() {
        Debug.Log("pointer is down");
        isDragging = true;
    }

    public void OnPointerUp() {
        Debug.Log("pointer is up");
        isDragging = false;
        SeekTrack(seekSlider.value);
    }

    string FormatTime(float time) {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return $"{minutes:D2}:{seconds:D2}"; 
    }

    void PopulatePlaylist()
    {
        foreach (AudioClip song in songQueue)
        {
            GameObject newSong = Instantiate(songItemPrefab, playlistPanel);
            int songNum = songQueue.IndexOf(song);
            newSong.GetComponentInChildren<TextMeshProUGUI>().text = $" {songNum+1}.  {song.name}   {FormatTime(song.length)}";

            newSong.GetComponent<Button>().onClick.AddListener(() => SelectSong(song));
        }
    }

    void SelectSong(AudioClip selectedSong)
    {
        clipNum = songQueue.IndexOf(selectedSong);
        PlaySong(clipNum);
    }

    void PlaySong(int index)
    {
        if (index < 0 || index >= songQueue.Count) return; // Safety check

        currentSong.clip = songQueue[index];
        currentSong.Play();
        songTitle.text = currentSong.clip.name;
    }

    public void AdjustVolume(float value) {
        currentSong.volume = value;
    }



}
