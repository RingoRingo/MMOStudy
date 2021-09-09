using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class NetWorkSocket : MonoBehaviour
{

    #region ����
    private static NetWorkSocket instance;
    public static NetWorkSocket Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("NetWorkSocket");
                DontDestroyOnLoad(obj);
                instance = obj.AddComponent<NetWorkSocket>();
            }

            return instance;
        }
    }
    #endregion
    /// <summary>
    /// �ͻ���socket
    /// </summary>
    private Socket m_Client;

    private byte[] buffer = new byte[10240];

    #region ������Ϣ�������
    /// <summary>
    /// ������Ϣ����
    /// </summary>
    private Queue<byte[]> m_SendQueue = new Queue<byte[]>();
    /// <summary>
    /// �����е�ί��
    /// </summary>
    private Action m_CheckSendQuene;
    #endregion

    #region ������Ϣ�������
    //�������ݰ����ֽ����黺����
    private byte[] m_ReceiveBuffer = new byte[10240];

    //�������ݰ��Ļ�����������
    private MMO_MemoryStream m_ReceiveMS = new MMO_MemoryStream();
    /// <summary>
    /// ������Ϣ�Ķ���
    /// </summary>
    private Queue<byte[]> m_ReceiveQueue = new Queue<byte[]>();

    private int m_ReceiveCount = 0;
    #endregion



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        #region �Ӷ����л�ȡ����
        while (true)
        {
            if (m_ReceiveCount <= 5)
            {
                m_ReceiveCount++;
                lock (m_ReceiveQueue)
                {
                    if (m_ReceiveQueue.Count > 0)
                    {
                        byte[] buffer = m_ReceiveQueue.Dequeue();

                        ushort protoCode = 0;
                        byte[] protoContent = new byte[buffer.Length - 2];

                        using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
                        {
                            //Э����
                            protoCode = ms.ReadUShort();
                            ms.Read(protoContent, 0, protoContent.Length);

                            EventDispatch.Instance.Dispatch(protoCode, protoContent);
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {
                m_ReceiveCount = 0;
                break;
            }
        }
        #endregion

    }
    private void OnDestroy()
    {
        if (m_Client!=null && m_Client.Connected)
        {
            m_Client.Shutdown(SocketShutdown.Both);
            m_CheckSendQuene = OnCheckSendQueueCallBack;
            m_Client.Close();
        }
    }

    #region OnCheckSendQueueCallBack �����е�ί�лص�
    /// <summary>
    /// �����е�ί�лص�
    /// </summary>
    private void OnCheckSendQueueCallBack()
    {
        lock (m_SendQueue)
        {
            //��������������ݰ����������ݰ�
            if (m_SendQueue.Count > 0)
            {
                //�������ݰ�
                Send(m_SendQueue.Dequeue());
            }
        }
    }
    #endregion


    #region ��װ���ݰ�
    /// <summary>
    /// ��װ���ݰ�
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    private byte[] MakeBuffer(byte[] data)
    {
        byte[] retBuffer = null;
        using (MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteUShort((ushort)data.Length);
            ms.Write(data, 0, data.Length);

            retBuffer = ms.ToArray();
        }
        return retBuffer;
    }
    #endregion

    #region SendMsg ������Ϣ������Ϣ�������
    /// <summary>
    /// ������Ϣ
    /// </summary>
    /// <param name="buffer"></param>
    public void SendMsg(byte[] buffer)
    {
        //�õ���װ������ݰ�
        byte[] sendBuffer = MakeBuffer(buffer);

        lock (m_SendQueue)
        {
            //�����ݰ��������
            m_SendQueue.Enqueue(sendBuffer);
            //����ί��(ִ��ί��)
            m_CheckSendQuene.BeginInvoke(null, null);
        }
    }
    #endregion

    #region ���ӵ�socket������
    /// <summary>
    /// ���ӵ�socket������
    /// </summary>
    /// <param name="ip">ip</param>
    /// <param name="port">�˿ں�</param>
    public void Connect(string ip, int port)
    {
        //���socket�Ѿ����ڣ����Ҵ�������״̬ ��ֱ�ӷ���
        if (m_Client != null && m_Client.Connected) return;

        m_Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            m_Client.Connect(new IPEndPoint(IPAddress.Parse(ip), port));
            m_CheckSendQuene = OnCheckSendQueueCallBack;

            ReceiveMsg();
        }
        catch (System.Exception ex)
        {
            Debug.Log("����ʧ��=" + ex.Message);
        }
    }

    #endregion

    #region Send �����ķ������ݰ���������
    /// <summary>
    /// �����ķ������ݰ���������
    /// </summary>
    /// <param name="buffer"></param>
    private void Send(byte[] buffer)
    {
        m_Client.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, SendCallBakc, m_Client);
    }
    #endregion

    /// <summary>
    /// �������ݰ��Ļص�
    /// </summary>
    /// <param name="ar"></param>
    private void SendCallBakc(IAsyncResult ar)
    {
        m_Client.EndSend(ar);

        //����������
        OnCheckSendQueueCallBack();
    }
    //=========================================================
    #region ReceiveMsg ��������
    /// <summary>
    /// ��������
    /// </summary>
    private void ReceiveMsg() {
        m_Client.BeginReceive(m_ReceiveBuffer, 0, m_ReceiveBuffer.Length, SocketFlags.None, ReceiveCallBack, m_Client);
    }
    #endregion
    #region ReceiveCallBack �������ݻص�
    /// <summary>
    /// �������ݻص�
    /// </summary>
    /// <param name="ar"></param>
    private void ReceiveCallBack(IAsyncResult ar)
    {
        try
        {
            int len = m_Client.EndReceive(ar);
            if (len > 0)
            {
                //�Ѿ����յ�������

                //�ѽ��յ����� д�뻺����������β��
                m_ReceiveMS.Position = m_ReceiveMS.Length;

                //��ָ�����ȵ��ֽ� д��������
                m_ReceiveMS.Write(m_ReceiveBuffer, 0, len);

                //��������������ĳ���>2��˵�������и��������İ�������
                //Ϊʲô������2 ��Ϊ���ǿͻ��˷�װ�� �õ�ushort����2
                if (m_ReceiveMS.Length > 2)
                {
                    //����ѭ�� ������ݰ�
                    while (true)
                    {
                        //��������ָ��λ�÷���0��
                        m_ReceiveMS.Position = 0;

                        //currMsgLen=����ĳ���
                        int currMsgLen = m_ReceiveMS.ReadUShort();

                        //currFullMsgLen �ܰ��ĳ���=��ͷ����+���峤��
                        int currFullMsgLen = 2 + currMsgLen;

                        //����������ĳ���>=��������  ˵�������յ���һ��������
                        if (m_ReceiveMS.Length >= currFullMsgLen)
                        {
                            //�����յ�һ��������

                            //��������byte[]����
                            byte[] buffer = new byte[currMsgLen];

                            //��������ָ��ŵ�2��λ�� Ҳ���ǰ����λ��
                            m_ReceiveMS.Position = 2;

                            //�Ѱ������byte[]����
                            m_ReceiveMS.Read(buffer, 0, currMsgLen);

                            lock (m_ReceiveQueue) {
                                m_ReceiveQueue.Enqueue(buffer);

                            }

                            //=================����ʣ���ֽ�����================
                            int remainLen = (int)m_ReceiveMS.Length - currFullMsgLen;
                            if (remainLen > 0)
                            {
                                //��ָ����ڵ�һ������β��
                                m_ReceiveMS.Position = currFullMsgLen;

                                //����ʣ���ֽ�����
                                byte[] remainBuffer = new byte[remainLen];

                                //������������ʣ���ֽ�����
                                m_ReceiveMS.Read(remainBuffer, 0, remainLen);

                                //���������
                                m_ReceiveMS.Position = 0;
                                m_ReceiveMS.SetLength(0);

                                //��ʣ���ֽ���������д��������
                                m_ReceiveMS.Write(remainBuffer, 0, remainBuffer.Length);

                                remainBuffer = null;
                            }
                            else
                            {
                                //û��ʣ���ֽ�

                                //���������
                                m_ReceiveMS.Position = 0;
                                m_ReceiveMS.SetLength(0);

                                break;
                            }

                        }
                        else
                        {
                            //��û���յ�������
                            break;
                        }
                    }
                }

                //������һ�ν������ݰ�
                ReceiveMsg();
            }
            else {
                //�ͻ��˶Ͽ�����
                Debug.Log(string.Format("������{0}�Ͽ�����", m_Client.RemoteEndPoint.ToString()));

            }
        }
        catch
        {
            //�ͻ��˶Ͽ�����
            Debug.Log(string.Format("������{0}�Ͽ�����", m_Client.RemoteEndPoint.ToString()));

        }
    }
    #endregion
}
