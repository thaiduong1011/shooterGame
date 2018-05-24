package vntd.demo.gamehdh.activities;

import android.app.Activity;
import android.app.Fragment;
import android.app.FragmentManager;
import android.app.FragmentTransaction;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.res.AssetManager;
import android.graphics.Typeface;
import android.os.Bundle;
import android.view.View;
import android.view.WindowManager;

import vntd.demo.gamehdh.GameView;
import vntd.demo.gamehdh.R;

public class MainActivity extends Activity {

    public static String userName;
    public static int userId;

    public  static final int GAME_WIDTH = 480;
    public static final int GAME_HEIGHT = 854;
    public static GameView sGame;
    public static AssetManager assets;
    private static SharedPreferences prefs;
    private static final String highScoreKey = "highScoreKey";
    private static int highScore;

    public static Typeface font;

    public FragmentManager fragmentManager = getFragmentManager();
    public static boolean ischangeFrag = false;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        Intent intent = getIntent();
        userName = intent.getStringExtra("user_name");
        userId = intent.getIntExtra("user_id", 0);

        prefs = getPreferences(Activity.MODE_PRIVATE);
        highScore = retrieveHighScore();
        assets = getAssets();
        font = Typeface.createFromAsset(assets,  "font.otf");
        sGame = new GameView(this, GAME_WIDTH, GAME_HEIGHT);
        if (ischangeFrag == false){
            addGameview();
        }else {
            addChangePass();
        }
        getWindow().addFlags(WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON);
    }

    @Override
    protected void onResume() {
        super.onResume();
        sGame.onResume();
    }

    @Override //onPause() is also run when the game is quit
    protected  void onPause() {
        super.onPause();
        sGame.onPause();
    }

    public static void setHighScore(int highScore) {
        MainActivity.highScore = highScore;
        SharedPreferences.Editor editor = prefs.edit();
        editor.putInt(highScoreKey, highScore);
        editor.commit();
    }

    private int retrieveHighScore() {
        return prefs.getInt(highScoreKey, 0);
    }

    public static int getHighScore() {
        return highScore;
    }

    public void addChangePass(){
        FragmentTransaction fragmentTransaction =fragmentManager.beginTransaction();
        fragmentTransaction.add(R.id.frameLayout, new ChangePassFragment(), "fragChangePass");
        fragmentTransaction.commit();
    }

    public void addGameview(){
        FragmentTransaction fragmentTransaction =fragmentManager.beginTransaction();
        fragmentTransaction.add(R.id.frameLayout, new GameviewFragment(), "fragGameview");
        fragmentTransaction.commit();
    }

    public void removeChangePass(View view){
        FragmentTransaction fragmentTransaction =fragmentManager.beginTransaction();
        ChangePassFragment changePassFragment = (ChangePassFragment) getFragmentManager().findFragmentByTag("fragChangePass");

        if (changePassFragment != null) {
            fragmentTransaction.remove(changePassFragment);
            fragmentTransaction.commit();
        }
    }

    public void removeGameView(View view){
        FragmentTransaction fragmentTransaction =fragmentManager.beginTransaction();
        Fragment gameviewFragment =  getFragmentManager().findFragmentByTag("fragGameview");

        if (gameviewFragment != null) {
            fragmentTransaction.remove(gameviewFragment);
            fragmentTransaction.commit();
        }
    }
}
