using MultitenancyPostgres.DataLayer;
using MultitenancyPostgres.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MultitenancyPostgres.Services.Memo
{
    public class MemoService
    {

        public async Task<Response> AddMemo(Models.MemoRequest @memorequest)
        {
            MemoRepository memoRepository = new MemoRepository("isuzu");
            Response response = new Response();
            var memo = new Models.Memo();
            memo.Content = memorequest.Content;
            memo.Title = memorequest.Title;
            memo.ContentType = memorequest.Attachment.ContentType;
            memo.CreatedOn = DateTime.Now;
            memo.ModifiedOn = DateTime.Now;
            if (memorequest.Attachment != null)
            {
                var uniqueFileName = memorequest.Attachment.FileName;
                // Specify a name for your top-level folder.
                string folderName = @"c:\Top-Level Folder";

                // To create a string that specifies the path to a subfolder under your
                // top-level folder, add a name for the subfolder to folderName.
                string pathString = System.IO.Path.Combine(folderName, "MemoFolder");
             
                System.IO.Directory.CreateDirectory(pathString);

                var filePath = Path.Combine(pathString, uniqueFileName);
                await memorequest.Attachment.CopyToAsync(new FileStream(filePath, FileMode.Create));

                memo.Attachmenturl = filePath;
            }
            else
            {
                memo.Attachmenturl = null;
            }

            var data =  memoRepository.AddMemo(memo);
            if (!data.IsFaulted)
            {
                response.Status = "00";
                response.Message = "Memo added successfuly";

            }
            else
            {
                response.Status = "01";
                response.Message = "An error occoured try again later";
            }

            return response;
        }

        
    }
}
