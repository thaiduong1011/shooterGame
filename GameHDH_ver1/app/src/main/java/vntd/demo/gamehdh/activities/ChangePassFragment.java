package vntd.demo.gamehdh.activities;

import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import vntd.demo.gamehdh.R;
import vntd.demo.gamehdh.async.AsyncChangePass;

public class ChangePassFragment extends android.app.Fragment {

    EditText edtOldPass, edtNewPass, edtConfirm;
    Button btnCancel, btnSubmit;



    @Nullable
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable final ViewGroup container, @Nullable Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_change_pass, container, false);

        AnhXa(view);
//
//        btnCancel.setOnClickListener(new View.OnClickListener() {
//            @Override
//            public void onClick(View v) {
//            }
//        });

        btnSubmit.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                String oldPass = edtOldPass.getText().toString();
                String newPass = edtNewPass.getText().toString();
                String confirmPass = edtConfirm.getText().toString();

                if (oldPass.equals("") || newPass.equals("") || confirmPass.equals("")){
                    Toast.makeText(getActivity(), "Please input full required infomation", Toast.LENGTH_SHORT).show();
                }else if (oldPass.equals(newPass)){
                    Toast.makeText(getActivity(), "Please input new password", Toast.LENGTH_SHORT).show();
                }else if (!newPass.equals(confirmPass)) {
                    Toast.makeText(getActivity(), "Please check password and comfirm password again", Toast.LENGTH_SHORT).show();
                }else {
                    oldPass = LoginActivity.convertPassMd5(oldPass);
                    newPass = LoginActivity.convertPassMd5(oldPass);

                    new AsyncChangePass(getActivity()).execute(MainActivity.userName, oldPass, newPass);
                }
            }
        });

        return view;
    }

    void AnhXa(View view){
        edtNewPass = view.findViewById(R.id.edtNewPass);
        edtOldPass = view.findViewById(R.id.edtOldPass);
        edtConfirm = view.findViewById(R.id.edtConfirmNewPass);

        btnCancel = view.findViewById(R.id.btnCancel);
        btnSubmit = view.findViewById(R.id.btnSubmit);
    }
}
