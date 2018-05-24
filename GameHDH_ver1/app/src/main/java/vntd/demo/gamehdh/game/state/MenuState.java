package vntd.demo.gamehdh.game.state;

import android.graphics.Color;
import android.graphics.Rect;
import android.util.Log;
import android.view.MotionEvent;

import vntd.demo.gamehdh.Assets;
import vntd.demo.gamehdh.activities.MainActivity;
import vntd.demo.gamehdh.async.AsyncScoreBoard;
import vntd.demo.gamehdh.framework.util.Painter;

public class MenuState extends State{
    Rect btnPlay, btnHighScore, btnQuit, btnChangePass;
    int btnWidth, btnHeight;

    public static int CLICK_SOUND = Assets.loadSound("clickSound.wav");
    
    
    @Override
    public void init() {
        btnWidth = Assets.playButton.getWidth();
        btnHeight = Assets.playButton.getHeight();
        btnPlay = new Rect(100, 200, 100 + btnWidth, 200 + btnHeight);
        btnHighScore = new Rect(100, 300, 100 + btnWidth, 300 + btnHeight);
        btnQuit = new Rect(100, 400, 100 + btnWidth, 400 + btnHeight);
        btnChangePass = new Rect(MainActivity.GAME_WIDTH - 270,
                MainActivity.GAME_HEIGHT-150,
                MainActivity.GAME_WIDTH - 30,
                MainActivity.GAME_HEIGHT - 40);
    }

    @Override
    public void update(float delta) {
    }

    @Override
    public void render(Painter g) {
        g.drawImage(Assets.menuBackground, 0, 0, MainActivity.GAME_WIDTH, MainActivity.GAME_HEIGHT);
        g.drawImage(Assets.playButton, 100, 200);
        g.drawImage(Assets.highScoreButton, 100, 300);
        g.drawImage(Assets.quitButton, 100, 400);

        g.setFont(30);
        g.setColor(Color.WHITE);
        g.drawRect(btnChangePass);
        g.drawString("Change Password", btnChangePass.left + 10, btnChangePass.top + 50);

    }

    @Override
    public boolean onTouch(MotionEvent e, int scaledX, int scaledY) {
        if (btnPlay.contains(scaledX, scaledY) ){
            MainActivity.sGame.setCurrentState(new LoadPlayState());
            Assets.playSound(CLICK_SOUND);
        }

        else if (btnHighScore.contains(scaledX, scaledY)){
            Assets.playSound(CLICK_SOUND);
            Log.d("AA", "da vao btn");
            //setCurrentState(new ScoreState());
            new AsyncScoreBoard(this).execute();
        }else if (btnQuit.contains(scaledX, scaledY)){
            Assets.playSound(CLICK_SOUND);
            System.exit(0);
        }else if (btnChangePass.contains(scaledX, scaledY)){
            MainActivity.ischangeFrag = true;
        }
        return true;

        //return false;
    }
}
