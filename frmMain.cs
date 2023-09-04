using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Text;


namespace PeakHourFactor
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class frmMain : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox txtPathName;
		private System.Windows.Forms.Button btnProcess;
		private System.Windows.Forms.TextBox txtDestFileName;
		private System.Windows.Forms.Label lblPath;
		private System.Windows.Forms.Label lblDestFile;
		private System.Windows.Forms.Button btnClose;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmMain()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.txtPathName = new System.Windows.Forms.TextBox();
			this.btnProcess = new System.Windows.Forms.Button();
			this.txtDestFileName = new System.Windows.Forms.TextBox();
			this.lblPath = new System.Windows.Forms.Label();
			this.lblDestFile = new System.Windows.Forms.Label();
			this.btnClose = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtPathName
			// 
			this.txtPathName.Location = new System.Drawing.Point(128, 16);
			this.txtPathName.Name = "txtPathName";
			this.txtPathName.Size = new System.Drawing.Size(144, 20);
			this.txtPathName.TabIndex = 0;
			this.txtPathName.Text = "";
			// 
			// btnProcess
			// 
			this.btnProcess.Location = new System.Drawing.Point(64, 112);
			this.btnProcess.Name = "btnProcess";
			this.btnProcess.Size = new System.Drawing.Size(128, 23);
			this.btnProcess.TabIndex = 2;
			this.btnProcess.Text = "Find PHF";
			this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
			// 
			// txtDestFileName
			// 
			this.txtDestFileName.Location = new System.Drawing.Point(128, 64);
			this.txtDestFileName.Name = "txtDestFileName";
			this.txtDestFileName.Size = new System.Drawing.Size(144, 20);
			this.txtDestFileName.TabIndex = 1;
			this.txtDestFileName.Text = "";
			// 
			// lblPath
			// 
			this.lblPath.Location = new System.Drawing.Point(24, 16);
			this.lblPath.Name = "lblPath";
			this.lblPath.Size = new System.Drawing.Size(104, 23);
			this.lblPath.TabIndex = 3;
			this.lblPath.Text = "Source Data Path:";
			// 
			// lblDestFile
			// 
			this.lblDestFile.Location = new System.Drawing.Point(24, 64);
			this.lblDestFile.Name = "lblDestFile";
			this.lblDestFile.Size = new System.Drawing.Size(100, 24);
			this.lblDestFile.TabIndex = 4;
			this.lblDestFile.Text = "Result File Name:";
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(232, 112);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(88, 23);
			this.btnClose.TabIndex = 3;
			this.btnClose.Text = "Exit";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// frmMain
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(344, 166);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.lblDestFile);
			this.Controls.Add(this.lblPath);
			this.Controls.Add(this.txtDestFileName);
			this.Controls.Add(this.btnProcess);
			this.Controls.Add(this.txtPathName);
			this.Name = "frmMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Find the Peak Hour Factor";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new frmMain());
		}

		private void btnProcess_Click(object sender, System.EventArgs e)
		{
			string sFileName, sPath;

			sPath = txtPathName.Text.Trim();

			if (sPath.Length == 0) 
				MessageBox.Show("You need to provide the directory name which contains the data files first.");
			else 
			{
				//Note: later on, may store the result in an access table.

				if (!Directory.Exists(sPath))
					MessageBox.Show("Please provide a valid path name.");
				else 
				{

					sFileName = txtDestFileName.Text.Trim();

					if (sFileName.Length == 0)
						MessageBox.Show("Need to provide the destination file name.");
					else 
					{
						if (File.Exists(sFileName))
							File.Delete(sFileName);
						StreamWriter strWriter = File.CreateText(sFileName);

						//now do the data processing work.

						//first get the file names and then process.
						try 
						{
							Console.WriteLine(sPath);
							DirectoryInfo di = new DirectoryInfo(@sPath); // what is @sPath??
							/*
							FileSystemInfo[] dirs = di.GetDirectories("*.*");
							Console.WriteLine("Number of directories with *.*: {0}", dirs.Length);
							// Count all the files in each subdirectory that contain the letter "e."
							foreach (DirectoryInfo diNext in dirs) 
							{
								Console.WriteLine("The number of files and directories " +
									"in {0} with an e is {1}", diNext, 
									diNext.GetFileSystemInfos("*e*").Length);
							}
							*/
							// Create an array representing the files in the current directory.
							FileInfo[] fi = di.GetFiles();
							Console.WriteLine("The following files exist in the current directory:");
							// Print out the names of the files in the current directory.
							foreach (FileInfo fiTemp in fi)
							{
								//Console.WriteLine(fiTemp.Name);
								try 
								{
									// Create an instance of StreamReader to read from a file.
									// The using statement also closes the StreamReader.
									//string myFile = fiTemp.Name;

									//Console.WriteLine("myFile: " + myFile);

									string newFile = sPath + "\\" + fiTemp.Name;

									//Console.WriteLine("newFile:" + newFile);

									//myFile = "D:\\course\\a.syn";

									
									using (StreamReader sr = new StreamReader(newFile)) 
									{
										string line;

										//string sResult = "filename: " + fiTemp.Name + " || ";
										string sResult="";
										int iCount = 0; //to check the number of records in the file

										bool bBiDirection = false;  //default is only data for one direction
										bool bFlowCountStart = false; //the indicator for checking if the actual data count has started;
										bool bDirectionFirstTime = true;
										//need to define several arrays to store the interim flow data
										string sSecondResult = " ";
										int [] clock = new int [24];
										int [] volume= new int [96];
										int [] volume2 = new int [96];
										int iIndex = 0;

										//the results are in the string sResult.

										while ((line = sr.ReadLine()) != null) 
										{
											//Now need to add the actual data processing code
											//Field are: all the information and the file name
											int iPos;

											line = line.ToUpper().Trim();
											if (line.Length == 0) continue;

											//Console.WriteLine(line);

											iPos = line.IndexOf("COUNTY:");
											if (iPos >= 0) 
											{
												iCount++;
												if (iCount > 1) //means the file contains more than one day's data
												{
													//write to the output file
												

													for (int i = 0; i < 96; i++)
													{
														//Console.WriteLine(sResult);
														//strWriter.WriteLine(sResult);
																											
														
														//Console.WriteLine("Result: " + sResult);
														strWriter.WriteLine(sResult+ "||"+volume2[i]);
										
													}



													if (bBiDirection)
													{
														
														for (int i = 0; i < 96; i++)
														{
															//Console.WriteLine("Second Result: " + sSecondResult);
															strWriter.WriteLine(sSecondResult+ "||"+volume2[i]);
										
														}
														
																									
														// Console.WriteLine(sSecondResult);
														// strWriter.WriteLine(sSecondResult);
													}

													//sResult = "filename: " + fiTemp.Name + " || ";
													sResult="";

													//remember to re-initialize all the variables
													bDirectionFirstTime = true;
													bFlowCountStart = false;
													bBiDirection = false;
													iIndex = 0;
												}
												string sCounty = line.Substring(iPos +7).Trim();

												//add data for the county name
												sResult += sCounty;
												continue;
											}
											//now handle station name
											iPos = line.IndexOf("STATION:");
											if (iPos >= 0)
											{
												string sStation = line.Substring(iPos + 8).Trim();
												sResult += sStation + " || ";
												continue;
											}
											
											//iPos = line.IndexOf("DESC");
											//if (iPos >= 0)
											//{
											//	string sDescription = line.Substring(iPos + 12).Trim();
											//	sResult += sDescription + " || ";
											//	continue;
											//}
											//now process start time & start date
											iPos = line.IndexOf("START");
											if (iPos >= 0)
											{
												string sSub = line.Substring(iPos + 5).Trim();
												iPos = sSub.IndexOf("DATE:");
												if (iPos >=0)
												{
													sResult += sSub.Substring(iPos + 5).Trim() + " || ";
													continue;
												}
												iPos = sSub.IndexOf("TIME:");
												if (iPos >= 0)
												{
													sResult += sSub.Substring(iPos + 5).Trim() + " || ";
													continue;
												}
											}
											//check if it contains data of two directions
                                            iPos = line.IndexOf("DIRECTION:");
											if (iPos >= 0)
											{
												if (bDirectionFirstTime)
												{
													if (bFlowCountStart == false)
														bFlowCountStart = true;
													string sSub = line.Substring(iPos + 10).Trim();
													iPos = sSub.IndexOf("DIRECTION:");
													if (iPos >= 0)
													{
														bBiDirection = true;
														sSecondResult = sResult;
														string sTemp = sSub.Substring(iPos + 10).Trim();
														sSecondResult += sTemp.Substring(0,1) + " || ";
													}
													sResult += sSub.Substring(0, 1) + " || ";
													bDirectionFirstTime = false;
												}
												
												continue;
											}

											//now handle data
											char ch = line[0];
											if (bFlowCountStart && char.IsDigit(ch))
											{
												iPos = line.IndexOf(" ");

												string sNum;
												sNum = line.Substring(0, 2).Trim();
												clock[iIndex] = int.Parse(sNum); //only need to store the clock for hour

												string sSub = line.Substring(iPos).Trim();
												for (int i = 0; i< 4; i++)
												{
													iPos = sSub.IndexOf(" ");
													sNum = sSub.Substring(0, iPos).Trim();
													volume[iIndex * 4 + i] = int.Parse(sNum);

													//sResult += " || " + sNum;

													sSub = sSub.Substring(iPos).Trim();
												}

												if (bBiDirection)
												{
													//same process for the other direction
													iPos = line.IndexOf("|");
													if (iPos < 0) 
													{
														MessageBox.Show("Error reading data, check file {1}", fiTemp.Name);
														this.Close();
													}
													sSub = line.Substring(iPos +1).Trim();
													for (int i = 0; i< 4; i++)
													{
														iPos = sSub.IndexOf(" ");
														sNum = sSub.Substring(0, iPos).Trim();
														volume2[iIndex * 4 + i] = int.Parse(sNum);

														//sResult += " || " + sNum;

														sSub = sSub.Substring(iPos).Trim();
													}
												}

												iIndex++;
												if (iIndex >= 24)
												{
													bFlowCountStart = false;
													//now need to compute peak hour factor
													//int iTotal1;
													//int iMax = 0, iHighest = 0;
													//int iMaxIndex = 0;
													//int iCurrPos = 0;

													//for (iCurrPos = 0; iCurrPos < 92; iCurrPos++)
													//{
														//iTotal1 = volume[iCurrPos] + volume[iCurrPos +1] + volume[iCurrPos + 2] + volume[iCurrPos + 3];
														//if (iTotal1 > iMax)
														//{
														//	iMax = iTotal1;
														//	iMaxIndex = iCurrPos;
														//}
													//}
													//for (iCurrPos = iMaxIndex; iCurrPos < iMaxIndex + 4; iCurrPos++)
													//	if (volume[iCurrPos] > iHighest) iHighest = volume[iCurrPos];

													//int iDiv = iMaxIndex/4;
													//sResult += clock[iDiv].ToString() + ":";
													//switch (iMaxIndex - iDiv * 4)
													//{
														//case 0: 
															//sResult += "00";
															//break;
														//case 1:
														//	sResult += "15";
														//	break;
														//case 2:
														//	sResult += "30";
														//	break;
														//case 3:
														//	sResult+= "45";
														//	break;
													//}
                                                   // sResult +=" || " + iMax.ToString() + " || " + iHighest.ToString() + " || ";

													//calculate PHF now
													//double PHF = (double)iMax / (double)(iHighest * 4);
													//sResult += string.Format("#.00", PHF) + " || ";
													//sResult += PHF.ToString() + " || ";

													//int iTotal2;
													//int iMax2 = 0;
													//int iMaxIndex2 = 0;
													//int iHighest2 = 0;
													//int iCurrPos2 = 0;

													//for (iCurrPos2 = 0; iCurrPos2 < 92; iCurrPos2++)
													//{
														//iTotal2 = volume2[iCurrPos2] + volume2[iCurrPos2 +1] + volume2[iCurrPos2 + 2] + volume2[iCurrPos2 + 3];
														//if (iTotal2 > iMax2)
														//{
														//	iMax2 = iTotal2;
														//	iMaxIndex2 = iCurrPos2;
														//}
													//}
													//for (iCurrPos2 = iMaxIndex2; iCurrPos2 < iMaxIndex2 + 4; iCurrPos2++)
													//	if (volume2[iCurrPos2] > iHighest2) iHighest2 = volume2[iCurrPos2];

													//int iDiv2 = iMaxIndex2/4;
													//sSecondResult += clock[iDiv2].ToString() + ":";
													//switch (iMaxIndex2 - iDiv2 * 4)
													//{
														//case 0: 
														//	sSecondResult += "00";
														//	break;
														//case 1:
														//	sSecondResult += "15";
														//	break;
														//case 2:
														//	sSecondResult += "30";
														//	break;
														//case 3:
														//	sSecondResult+= "45";
														//	break;
													//}
													//sSecondResult +=" || " + iMax2.ToString() + " || " + iHighest2.ToString() + " || ";
													//double PHF2 = (double)iMax2 / (double)(iHighest2 * 4);
													//sSecondResult += string.Format("#.000", PHF2) + " || ";
													//sSecondResult += PHF2 + " || ";

												} //of if (index <=24)
												continue;
											}
											//get truck percentage data
											iPos = line.IndexOf("TRUCK");
											if (iPos >= 0)
											{
												//add processing later.
												iPos = line.IndexOf("PERCENTAGE");
												string sSub = line.Substring(iPos + 10).Trim();
												iPos = sSub.IndexOf(" ");
												string sTruck = sSub.Substring(0, iPos);
												sResult += sTruck;
												if (bBiDirection)
												{
													sSub = sSub.Substring(iPos +1).Trim();
													iPos = sSub.IndexOf(" ");
													string sTruck2 = sSub.Substring(0, iPos);
													sSecondResult += sTruck2;
												}
												continue;
											}
										} //end of readline
										Console.WriteLine("Result string: " + sResult);
										


										for (int i = 0; i < 96; i++)
										{
											strWriter.WriteLine(sResult+ "||" +volume[i]);
										
										}
										
																				
										
									    strWriter.WriteLine(sResult);
										
										
										
										if (bBiDirection)
										{
											//Console.WriteLine("Second Result: " + sSecondResult);
											//strWriter.WriteLine(sSecondResult);

											for (int i = 0; i < 96; i++)
											{
												Console.WriteLine("Second Result: " + sSecondResult);
												strWriter.WriteLine(sSecondResult+ "||"+volume2[i]);
										
											}
										}

										//later on, write it to the destination file

									} //end of read file
									
								}

								catch (Exception newex) 
								{
									// Let the user know what went wrong.
									Console.WriteLine("The file could not be read:");
									Console.WriteLine(newex.Message);
								}
							} //end of the second try
							//continue to process
							strWriter.Close();

                            MessageBox.Show("File processing has already finished");
						} 
						catch (Exception ex) 
						{
							Console.WriteLine("The process failed: {0}", ex.ToString());
						}                        

					}  //end of the first try
				}
			}

		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}
