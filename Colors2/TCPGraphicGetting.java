import java.io.*;
import java.net.ServerSocket;
import java.net.Socket;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.net.Inet4Address;
import java.net.InetAddress;
import java.net.NetworkInterface;
import java.net.SocketException;
import java.util.Collections;
import java.nio.ByteBuffer;

public class TCPGraphicGetting extends Thread{

    //public TCPGraphicGetting(){}
    public static void main(String args[]) {
        System.out.println("TCPサーバ起動");
        int file_num = 0;
        Date date = new Date();
        SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MMdd-HHmm");
        final int PORT = 11451;
        long size=0;

        ServerSocket serverSocket = null;
        while (true) {
            byte[] data = new byte[16];
            byte[] size_buf = new byte[64];
            int len;
            int num=0;
            try {
                // 新規サーバーソケットの生成
                if( serverSocket == null ){
                    serverSocket = new ServerSocket(PORT);
                    System.out.println("サーバソケット作成");
                    for(NetworkInterface n: Collections.list(NetworkInterface.getNetworkInterfaces()) ) {
                        for (InetAddress addr : Collections.list(n.getInetAddresses())) {
                            if( addr instanceof Inet4Address && !addr.isLoopbackAddress() ){
                                 System.out.println("IP: "+ addr.getHostAddress() +"  PORT: "+ PORT);
                            }
                        }
                    }
                }
                // クライアントからの接続を待ちます
                System.out.println("待機中");
                Socket socket = serverSocket.accept();
                File file = new File( "drawImages\\" + sdf.format(date) + "__" + file_num + ".png");
                OutputStream out = new FileOutputStream(file);
                InputStream in = socket.getInputStream();
                System.out.println("画像取得中 : "+"drawSource\\" + sdf.format(date) + "__" + file_num + ".png");
                size = 0;
                for(int i=0;i<4;i++){
                    in.read(size_buf, 0, 16);
                    size += ByteBuffer.wrap(size_buf).getLong()<<16*i;
                }
                System.out.println(size);
                while (num < size) {
                    if((len = in.read(data,0,16)) == -1){
                        out.flush();
                        out.close();
                        in.close();
                        file.delete();
                        throw new EOFException("File取得中の情報欠損エラー");
                    }
                    out.write(data,0,len);
                    num += len;
	            System.out.println("残り"+((double)num*100/size)+"%");
                }
                // 入出力ストリームを閉じる
                out.flush();
                out.close();
                in.close();
                System.out.println("done");
                // ソケットを閉じる
                socket.close();
                file_num++;
            }catch(Exception e){
                    e.printStackTrace();
            }
        }
    }
}
