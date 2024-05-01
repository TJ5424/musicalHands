Sitar sit => dac;
0.75 => sit.gain;
[50, 52, 54, 57, 59, 62, 64] @=> int notes[];

while (true) {
    Math.random2(0, notes.cap() - 1) => int randomIndex;
    notes[randomIndex] => int note;
    Std.mtof(note) => sit.freq;
    1 => sit.noteOn;
    Math.random2f(0.2,0.8)::second => now;
}
